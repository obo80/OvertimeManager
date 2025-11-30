using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Queries.GetAllCompensationsByEmployeId
{
    public class GetAllCompensationsByEmployeIdQueryHandler : IRequestHandler<GetAllCompensationsByEmployeIdQuery, IEnumerable<GetCompensationDto>>
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IMapper _mapper;

        public GetAllCompensationsByEmployeIdQueryHandler(ICompensationRepository compensationRepository, IMapper mapper)
        {
            _compensationRepository = compensationRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetCompensationDto>> Handle(GetAllCompensationsByEmployeIdQuery request, CancellationToken cancellationToken)
        {
            var compensations = await _compensationRepository.GetAllForEmployeeIdAsync(request.EmployeeId);
            var compensationsDto = _mapper.Map<List<GetCompensationDto>>(compensations);
            return compensationsDto;
        }
    }


}