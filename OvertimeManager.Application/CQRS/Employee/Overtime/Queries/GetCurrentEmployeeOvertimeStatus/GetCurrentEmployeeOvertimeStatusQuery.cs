using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Queries.GetCurrentEmployeeOvertimeStatus
{
    public class GetCurrentEmployeeOvertimeStatusQuery : IRequest<EmployeeOvertimeStatusDto>
    {

        public int CurrentEmployeeId { get; }

        public GetCurrentEmployeeOvertimeStatusQuery(int currentEmployeeId)
        {
            CurrentEmployeeId = currentEmployeeId;
        }
    }
}
