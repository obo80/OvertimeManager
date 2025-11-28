using MediatR;
using OvertimeManager.Api.Controllers;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeesOvertimeStatus
{
    public class GetCurrentManagerEmployeesOvertimeStatusQuery : IRequest<IEnumerable<EmployeeOvertimeStatusDto>>
    {
        public GetCurrentManagerEmployeesOvertimeStatusQuery(int currentManagerId)
        {
            CurrentManagerId = currentManagerId;
        }

        public int CurrentManagerId { get; }
    }
}
