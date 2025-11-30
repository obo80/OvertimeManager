using MediatR;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeeByIdCompensationStatus
{
    public class GetCurrentManagerEmployeeByIdCompensationStatusQuery : IRequest<EmployeeCompensationStatusDto>
    {
        public GetCurrentManagerEmployeeByIdCompensationStatusQuery(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            EmployeeId = id;
        }

        public int CurrentManagerId { get; }
        public int EmployeeId { get; }
    }


}