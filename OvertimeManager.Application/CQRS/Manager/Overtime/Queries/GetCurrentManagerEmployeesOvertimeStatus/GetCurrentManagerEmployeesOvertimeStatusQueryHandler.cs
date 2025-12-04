using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeesOvertimeStatus
{
    public class GetCurrentManagerEmployeesOvertimeStatusQueryHandler : 
        IRequestHandler<GetCurrentManagerEmployeesOvertimeStatusQuery, PagedResult<EmployeeOvertimeStatusDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMediator _mediator;

        public GetCurrentManagerEmployeesOvertimeStatusQueryHandler(IEmployeeRepository employeeRepository, IMediator mediator)
        {
            _employeeRepository = employeeRepository;
            _mediator = mediator;
        }
        public async Task<PagedResult<EmployeeOvertimeStatusDto>> Handle(
            GetCurrentManagerEmployeesOvertimeStatusQuery request, CancellationToken cancellationToken)
        {
            var manager = await _employeeRepository.GetByIdAsync(request.CurrentManagerId);
            if (manager == null)
                throw new NotFoundException("Manager not found", request.CurrentManagerId.ToString());

            var (employees, totalCount) = await _employeeRepository.GetAllByManagerIdAsync(manager.Id, request.QueryOptions);

            List<EmployeeOvertimeStatusDto> employeeStatusDtos = new List<EmployeeOvertimeStatusDto>();
            foreach (var employee in employees)
            {
                //direct mapping, more efficient way to get each employee status in a single call to db for all employees
                var statusDto = new EmployeeOvertimeStatusDto()
                {
                    EmployeeId = employee.Id,
                    EmployeeEmail = employee.Email,
                    TakenOvertime = employee.OvertimeSummary.TakenOvertime,
                    SettledOvertime = employee.OvertimeSummary.SettledOvertime,
                    UnsettledOvertime = employee.OvertimeSummary.UnsettledOvertime
                };

                employeeStatusDtos.Add(statusDto);
            }
            var result = new PagedResult<EmployeeOvertimeStatusDto>(employeeStatusDtos, totalCount, 
                request.QueryOptions.PageSize, request.QueryOptions.PageNumber);

            return result;
        }
    }
}
