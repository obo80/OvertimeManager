using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Queries.GetAllActiveOvertimesByEmployeeId
{
    public class GetAllActiveOvertimesByEmployeeIdQuery : IRequest<PagedResult<GetOvertimeDto>>
    {
        public GetAllActiveOvertimesByEmployeeIdQuery(int employeeId, FromQueryOptions queryOptions)
        {
            QueryOptions = queryOptions;
            EmployeeId = employeeId;
        }

        public int EmployeeId { get; }
        public FromQueryOptions QueryOptions { get; }
    }
}