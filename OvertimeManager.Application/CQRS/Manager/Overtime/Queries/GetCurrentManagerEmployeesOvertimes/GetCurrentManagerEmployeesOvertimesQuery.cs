using MediatR;
using OvertimeManager.Application.CQRS.Manager.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeesOvertimes
{
    public class GetCurrentManagerEmployeesOvertimesQuery : IRequest<IEnumerable<EmployeeOvertimeRequestsDto>>
    {
        public GetCurrentManagerEmployeesOvertimesQuery(int currentManagerId)
        {
            CurrentManagerId = currentManagerId;
        }

        public int CurrentManagerId { get; }
    }
}