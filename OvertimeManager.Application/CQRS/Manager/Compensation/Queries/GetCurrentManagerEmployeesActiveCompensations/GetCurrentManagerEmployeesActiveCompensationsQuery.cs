using MediatR;
using OvertimeManager.Api.Controllers;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeesActiveCompensations
{
    public class GetCurrentManagerEmployeesActiveCompensationsQuery : IRequest<PagedResult<GetCompensationDto>>
    {
        public GetCurrentManagerEmployeesActiveCompensationsQuery(int currentManagerId, FromQueryOptions queryOptions)
        {
            CurrentManagerId = currentManagerId;
            QueryOptions = queryOptions;
        }

        public int CurrentManagerId { get; }
        public FromQueryOptions QueryOptions { get; }
    }
}