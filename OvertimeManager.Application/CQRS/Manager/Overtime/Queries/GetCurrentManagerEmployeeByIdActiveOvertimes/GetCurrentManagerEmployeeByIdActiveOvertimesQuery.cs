using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeeByIdActiveOvertimes
{
    public class GetCurrentManagerEmployeeByIdActiveOvertimesQuery : IRequest<PagedResult<GetOvertimeDto>>
    {
        public GetCurrentManagerEmployeeByIdActiveOvertimesQuery(int currentManagerId, int id, FromQueryOptions queryOptions)
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