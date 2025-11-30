using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Queries.GetAllOvertimesByEmployeId
{
    public class GetAllOvertimesByEmployeIdQuery : IRequest<IEnumerable<GetOvertimeDto>>
    {
        public int EmployeeId { get; }
        public GetAllOvertimesByEmployeIdQuery(int employeeId)
        {
            EmployeeId = employeeId;
        }

    }
}
