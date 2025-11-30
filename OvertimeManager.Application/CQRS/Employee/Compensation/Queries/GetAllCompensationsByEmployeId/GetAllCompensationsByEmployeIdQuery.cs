using MediatR;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Queries.GetAllCompensationsByEmployeId
{
    public class GetAllCompensationsByEmployeIdQuery : IRequest<IEnumerable<GetCompensationDto>>
    {
        public GetAllCompensationsByEmployeIdQuery(int employeeId)
        {
            EmployeeId = employeeId;
        }

        public int EmployeeId { get; }
    }


}