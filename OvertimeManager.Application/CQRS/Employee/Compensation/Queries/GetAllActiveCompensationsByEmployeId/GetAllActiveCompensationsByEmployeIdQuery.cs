using MediatR;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Queries.GetAllActiveCompensationsByEmployeId
{
    public class GetAllActiveCompensationsByEmployeIdQuery : IRequest<IEnumerable<GetCompensationDto>>
    {
        public GetAllActiveCompensationsByEmployeIdQuery(int employeeId)
        {
            EmployeeId = employeeId;
        }

        public int EmployeeId { get; }
    }
}