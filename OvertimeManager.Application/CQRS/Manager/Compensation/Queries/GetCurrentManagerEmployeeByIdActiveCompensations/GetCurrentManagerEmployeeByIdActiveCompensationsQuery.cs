using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeeByIdActiveCompensations
{
    public class GetCurrentManagerEmployeeByIdActiveCompensationsQuery : IRequest<PagedResult<GetCompensationDto>>
    {
        public GetCurrentManagerEmployeeByIdActiveCompensationsQuery(int currentManagerId, int id, FromQueryOptions queryOptions)
        {
            CurrentManagerId = currentManagerId;
            EmployeeId = id;
            QueryOptions = queryOptions;
        }

        public int CurrentManagerId { get; }
        public int EmployeeId { get; }
        public FromQueryOptions QueryOptions { get; }
    }
}