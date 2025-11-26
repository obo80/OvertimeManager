using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Api.Controllers
{
    public class GetAllActiveOvertimesByEmployeIdQuery : IRequest<IEnumerable<GetOvertimeDto>>
    {
        public GetAllActiveOvertimesByEmployeIdQuery(int employeeId)
        {
            EmployeeId = employeeId;
        }

        public int EmployeeId { get; }
    }
}