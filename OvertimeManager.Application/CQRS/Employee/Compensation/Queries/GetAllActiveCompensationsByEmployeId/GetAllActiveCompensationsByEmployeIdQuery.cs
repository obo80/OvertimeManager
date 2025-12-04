using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Queries.GetAllActiveCompensationsByEmployeId
{
    public class GetAllActiveCompensationsByEmployeIdQuery : IRequest<PagedResult<GetCompensationDto>>
    {
        public GetAllActiveCompensationsByEmployeIdQuery(int employeeId, FromQueryOptions queryOptions)
        {
            EmployeeId = employeeId;
            QueryOptions = queryOptions;
        }

        public int EmployeeId { get; }
        public FromQueryOptions QueryOptions { get; }
    }
}