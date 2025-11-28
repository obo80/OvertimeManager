using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Queries.GetAllActiveCompensationsByEmployeId
{
    public class GetAllActiveCompensationsByEmployeIdQuery : IRequest<IEnumerable<GetCompensationDto>>
    {
        public GetAllActiveCompensationsByEmployeIdQuery(int employeeId)
        {
            EmployeeId = employeeId;
        }

        public int EmployeeId { get; }
    }

    public class GetAllActiveCompensationsByEmployeIdQueryHandler : IRequestHandler<GetAllActiveCompensationsByEmployeIdQuery, IEnumerable<GetCompensationDto>>
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IMapper _mapper;

        public GetAllActiveCompensationsByEmployeIdQueryHandler(ICompensationRepository compensationRepository, IMapper mapper)
        {
            _compensationRepository = compensationRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetCompensationDto>> Handle(GetAllActiveCompensationsByEmployeIdQuery request, CancellationToken cancellationToken)
        {
            var compensations = await _compensationRepository.GetAllActiveForEmployeeIdAsync(request.EmployeeId);
            var compensationsDto = _mapper.Map<List<GetCompensationDto>>(compensations);
            return compensationsDto;
        }
    }
}