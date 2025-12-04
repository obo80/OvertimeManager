using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Queries.GetAllOvertimesByEmployeId
{
    public class GetAllOvertimesByEmployeIdQuery : IRequest<PagedResult<GetOvertimeDto>>
    {
        public int EmployeeId { get; }
        public FromQueryOptions QueryOptions { get; }



        public GetAllOvertimesByEmployeIdQuery(int employeeId, FromQueryOptions queryOptions)
        {
            QueryOptions = queryOptions;
            EmployeeId = employeeId;
        }
    }
}
