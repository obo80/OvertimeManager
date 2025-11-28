using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Queries.GetCompensationById
{
    public class GetCompensationByIdQuery : IRequest<GetCompensationDto>
    {
        public GetCompensationByIdQuery(int currentEmployeeId, int id)
        {
            CurrentEmployeeId = currentEmployeeId;
            CompensationId = id;
        }

        public int CurrentEmployeeId { get; }
        public int CompensationId { get; }
    }

    public class GetCompensationByIdQueryHandler : IRequestHandler<GetCompensationByIdQuery, GetCompensationDto>
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IMapper _mapper;

        public GetCompensationByIdQueryHandler(ICompensationRepository compensationRepository, IMapper mapper)
        {
            _compensationRepository = compensationRepository;
            _mapper = mapper;
        }
        public async Task<GetCompensationDto> Handle(GetCompensationByIdQuery request, CancellationToken cancellationToken)
        {
            var compensation = await _compensationRepository.GetByIdAsync(request.CompensationId);
            if (compensation == null)
                throw new NotFoundException("Compensation request not found.", request.CompensationId.ToString());

            var compensationEmployeeId = compensation.RequestedForEmployeeId;
            if (compensationEmployeeId != request.CurrentEmployeeId)
                throw new UnauthorizedException("You are not authorized to get this compensation request.");

            var getCompensationDto = _mapper.Map<GetCompensationDto>(compensation);
            return getCompensationDto;
        }
    }
}