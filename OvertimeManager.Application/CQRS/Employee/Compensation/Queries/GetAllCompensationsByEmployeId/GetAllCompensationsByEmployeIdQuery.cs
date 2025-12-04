using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Queries.GetAllCompensationsByEmployeId
{
    public class GetAllCompensationsByEmployeIdQuery : IRequest<PagedResult<GetCompensationDto>>
    {
        public GetAllCompensationsByEmployeIdQuery(int employeeId, FromQueryOptions queryOptions)
        {
            EmployeeId = employeeId;
            QueryOptions = queryOptions;
        }

        public int EmployeeId { get; }
        public FromQueryOptions QueryOptions { get; }
    }


}