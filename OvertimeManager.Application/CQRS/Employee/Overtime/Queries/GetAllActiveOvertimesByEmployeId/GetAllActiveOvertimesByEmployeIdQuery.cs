using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Api.Controllers
{
    public class GetAllActiveOvertimesByEmployeIdQuery : IRequest<PagedResult<GetOvertimeDto>>
    {
        public GetAllActiveOvertimesByEmployeIdQuery(int employeeId, FromQueryOptions queryOptions)
        {
            QueryOptions = queryOptions;
            EmployeeId = employeeId;
        }

        public int EmployeeId { get; }
        public FromQueryOptions QueryOptions { get; }
    }
}