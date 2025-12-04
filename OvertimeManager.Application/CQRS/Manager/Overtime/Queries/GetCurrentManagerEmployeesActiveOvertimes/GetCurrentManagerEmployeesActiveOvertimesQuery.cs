using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Application.CQRS.Manager.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeesActiveOvertimes
{
    public class GetCurrentManagerEmployeesActiveOvertimesQuery : IRequest<PagedResult<GetOvertimeDto>>
    {
        public GetCurrentManagerEmployeesActiveOvertimesQuery(int currentManagerId, FromQueryOptions queryOptions)
        {
            CurrentManagerId = currentManagerId;
            QueryOptions = queryOptions;
        }

        public int CurrentManagerId { get; }
        public FromQueryOptions QueryOptions { get; }
    }
    


}