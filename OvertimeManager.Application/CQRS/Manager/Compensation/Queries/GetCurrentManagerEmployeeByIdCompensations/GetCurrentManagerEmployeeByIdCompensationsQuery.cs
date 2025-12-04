using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeeByIdCompensations
{
    public class GetCurrentManagerEmployeeByIdCompensationsQuery : IRequest<PagedResult<GetCompensationDto>>
    {
        public GetCurrentManagerEmployeeByIdCompensationsQuery(int currentManagerId, int id, FromQueryOptions queryOptions)
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