using MediatR;
using OvertimeManager.Application.CQRS.Manager.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeesActiveOvertimes
{
    public class GetCurrentManagerEmployeesActiveOvertimesQuery : IRequest<IEnumerable<EmployeeOvertimeRequestsDto>>
    {
        public GetCurrentManagerEmployeesActiveOvertimesQuery(int currentManagerId)
        {
            CurrentManagerId = currentManagerId;
        }

        public int CurrentManagerId { get; }
    }
    


}