using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Application.CQRS.Manager.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeesOvertimes
{
    public class GetCurrentManagerEmployeesOvertimesQuery : IRequest<PagedResult<GetOvertimeDto>>
    {
        public GetCurrentManagerEmployeesOvertimesQuery(int currentManagerId, FromQueryOptions queryOptions)
        {
            CurrentManagerId = currentManagerId;
            QueryOptions = queryOptions;
        }

        public int CurrentManagerId { get; }
        public FromQueryOptions QueryOptions { get; }
    }
}