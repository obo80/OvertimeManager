using MediatR;
using OvertimeManager.Api.Controllers;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeesCompensations
{
    public class GetCurrentManagerEmployeesCompensationsQuery : IRequest<IEnumerable<EmployeeCompensationRequestsDto>>
    {
        public GetCurrentManagerEmployeesCompensationsQuery(int currentManagerId)
        {
            CurrentManagerId = currentManagerId;
        }

        public int CurrentManagerId { get; }
    }
}