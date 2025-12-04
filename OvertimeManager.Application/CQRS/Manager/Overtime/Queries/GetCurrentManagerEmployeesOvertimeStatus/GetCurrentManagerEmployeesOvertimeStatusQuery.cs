using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeesOvertimeStatus
{
    public class GetCurrentManagerEmployeesOvertimeStatusQuery : IRequest<PagedResult<EmployeeOvertimeStatusDto>>
    {
        public GetCurrentManagerEmployeesOvertimeStatusQuery(int currentManagerId, FromQueryOptions queryOptions)
        {
            CurrentManagerId = currentManagerId;
            QueryOptions = queryOptions;
        }

        public int CurrentManagerId { get; }
        public FromQueryOptions QueryOptions { get; }
    }
}
