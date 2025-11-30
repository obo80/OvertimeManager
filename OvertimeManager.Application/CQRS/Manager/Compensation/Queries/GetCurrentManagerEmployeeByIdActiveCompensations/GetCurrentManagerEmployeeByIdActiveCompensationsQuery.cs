using MediatR;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeeByIdActiveCompensations
{
    public class GetCurrentManagerEmployeeByIdActiveCompensationsQuery : IRequest<IEnumerable<GetCompensationDto>>
    {
        public GetCurrentManagerEmployeeByIdActiveCompensationsQuery(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            EmployeeId = id;
        }

        public int CurrentManagerId { get; }
        public int EmployeeId { get; }
    }
}