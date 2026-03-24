using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Queries.GetAllOvertimesByEmployeeId
{
    public class GetAllOvertimesByEmployeeIdQuery : IRequest<PagedResult<GetOvertimeDto>>
    {
        public int EmployeeId { get; }
        public FromQueryOptions QueryOptions { get; }



        public GetAllOvertimesByEmployeeIdQuery(int employeeId, FromQueryOptions queryOptions)
        {
            QueryOptions = queryOptions;
            EmployeeId = employeeId;
        }
    }
}
