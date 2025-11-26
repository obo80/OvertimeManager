using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Queries.GetCurrentManagerEmployeesOvertimeStatus
{
    public class GetCurrentManagerEmployeesOvertimeStatusQueryHandler : IRequestHandler<GetCurrentManagerEmployeesOvertimeStatusQuery, IEnumerable<EmployeeOvertimeStatusDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMediator _mediator;

        public GetCurrentManagerEmployeesOvertimeStatusQueryHandler(IEmployeeRepository employeeRepository, IMediator mediator)
        {
            _employeeRepository = employeeRepository;
            _mediator = mediator;
        }
        public async Task<IEnumerable<EmployeeOvertimeStatusDto>> Handle(GetCurrentManagerEmployeesOvertimeStatusQuery request, CancellationToken cancellationToken)
        {
            var manager = await _employeeRepository.GetByIdAsync(request.CurrentManagerId);
            if (manager == null)
                throw new NotFoundException("Manager not found", request.CurrentManagerId.ToString());

            var employees = await _employeeRepository.GetAllByManagerIdAsync(manager.Id);

            List<EmployeeOvertimeStatusDto> statusDtos = new List<EmployeeOvertimeStatusDto>();
            foreach (var employee in employees)
            {
                //altinative way using mediator to get each employee status, but less efficient, because of multiple calls to db, so we use direct mapping below
                //var statusDto = await _mediator.Send(new GetCurrentManagerEmployeeByIdOvertimeStatusQuery(request.CurrentManagerId, employee.Id));

                //direct mapping, more efficient way to get each employee status in a single call to db for all employees
                var statusDto = new EmployeeOvertimeStatusDto()
                {
                    EmployeeId = employee.Id,
                    EmployeeEmail = employee.Email,
                    TakenOvertime = employee.OvertimeSummary.TakenOvertime,
                    SettledOvertime = employee.OvertimeSummary.SettledOvertime,
                    UnsettledOvertime = employee.OvertimeSummary.UnsettledOvertime
                };

                statusDtos.Add(statusDto);
            }
            return statusDtos;
        }
    }
}
