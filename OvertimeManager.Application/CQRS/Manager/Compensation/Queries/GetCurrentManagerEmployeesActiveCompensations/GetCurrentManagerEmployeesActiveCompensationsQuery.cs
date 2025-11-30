using MediatR;
using OvertimeManager.Api.Controllers;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeesActiveCompensations
{
    public class GetCurrentManagerEmployeesActiveCompensationsQuery : IRequest<IEnumerable<EmployeeCompensationRequestsDto>>
    {
        public GetCurrentManagerEmployeesActiveCompensationsQuery(int currentManagerId)
        {
            CurrentManagerId = currentManagerId;
        }

        public int CurrentManagerId { get; }
    }
}