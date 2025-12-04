using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.HR.Employees.DTOs;

namespace OvertimeManager.Application.CQRS.HR.Employees.Queries.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<PagedResult<HREmployeeWithOvetimeDataDto>>
    {


        public GetAllEmployeesQuery(FromQueryOptions queryOptions)
        {
            QueryOptions = queryOptions;
        }

        public FromQueryOptions QueryOptions { get; }
    }
}
