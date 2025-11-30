using MediatR;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeeByIdCompensations
{
    public class GetCurrentManagerEmployeeByIdCompensationsQuery : IRequest<IEnumerable<GetCompensationDto>>
    {
        public GetCurrentManagerEmployeeByIdCompensationsQuery(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            EmployeeId = id;
        }

        public int CurrentManagerId { get; }
        public int EmployeeId { get; }
    }


}