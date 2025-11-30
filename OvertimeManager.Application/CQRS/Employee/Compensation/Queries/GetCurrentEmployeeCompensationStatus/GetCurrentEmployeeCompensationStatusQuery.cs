using MediatR;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Queries.GetCurrentEmployeeCompensationStatus
{
    public class GetCurrentEmployeeCompensationStatusQuery : IRequest<EmployeeCompensationStatusDto>
    {
        public int CurrentEmployeeId { get; }
        public GetCurrentEmployeeCompensationStatusQuery(int currentEmployeeId)
        {
            CurrentEmployeeId = currentEmployeeId;
        }
    }
}

