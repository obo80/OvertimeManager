using AutoMapper;
using MediatR;
using OvertimeManager.Api.Controllers;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Application.CQRS.Manager.Overtime.DTOs;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeesActiveCompensations
{
    public class GetCurrentManagerEmployeesActiveCompensationsQuery : IRequest<IEnumerable<EmployeeCompensationRequestsDto>>
    {
        public GetCurrentManagerEmployeesActiveCompensationsQuery(int currentManagerId)
        {
            CurrentManagerId = currentManagerId;
        }

        public int CurrentManagerId { get; }
    }

    public class GetCurrentManagerEmployeesActiveCompensationsQueryHanlder :
        IRequestHandler<GetCurrentManagerEmployeesActiveCompensationsQuery, IEnumerable<EmployeeCompensationRequestsDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompensationRepository _compensationRepository;
        private readonly IMapper _mapper;

        public GetCurrentManagerEmployeesActiveCompensationsQueryHanlder(IEmployeeRepository employeeRepository,
            ICompensationRepository compensationRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _compensationRepository = compensationRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeCompensationRequestsDto>> Handle(GetCurrentManagerEmployeesActiveCompensationsQuery request, CancellationToken cancellationToken)
        {
            var manager = await _employeeRepository.GetByIdAsync(request.CurrentManagerId);
            if (manager == null)
                throw new NotFoundException("Manager not found", request.CurrentManagerId.ToString());

            var employees = await _employeeRepository.GetAllByManagerIdAsync(manager.Id);

            var employeeCompensationRequestsDtos = new List<EmployeeCompensationRequestsDto>();
            foreach (var employee in employees)
            {
                var compensations = await _compensationRepository.GetAllActiveForEmployeeIdAsync(employee.Id);
                var compensationsDto = _mapper.Map<List<GetCompensationDto>>(compensations);
                employeeCompensationRequestsDtos.Add(new EmployeeCompensationRequestsDto
                {
                    EmployeeId = employee.Id,
                    EmployeeEmail = employee.Email,
                    CompensationRequest = compensationsDto,
                });
            }
            return employeeCompensationRequestsDtos;
        }
    }
}