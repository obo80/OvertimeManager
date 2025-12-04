using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeeByIdOvertimes
{
    public class GetCurrentManagerEmployeeByIdOvertimesQuery : IRequest<PagedResult<GetOvertimeDto>>
    {
        public GetCurrentManagerEmployeeByIdOvertimesQuery(int currentManagerId, int id, FromQueryOptions queryOptions)
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