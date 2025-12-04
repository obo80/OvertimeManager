using MediatR;
using Microsoft.AspNetCore.Mvc;
using OvertimeManager.Api.Controllers;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeesCompensations
{
    public class GetCurrentManagerEmployeesCompensationsQuery : IRequest<PagedResult<GetCompensationDto>>
    {
        public GetCurrentManagerEmployeesCompensationsQuery(int currentManagerId, FromQueryOptions queryOptions)
        {
            CurrentManagerId = currentManagerId;
            QueryOptions = queryOptions;
        }

        public int CurrentManagerId { get; }
        public FromQueryOptions QueryOptions { get; }
    }
}