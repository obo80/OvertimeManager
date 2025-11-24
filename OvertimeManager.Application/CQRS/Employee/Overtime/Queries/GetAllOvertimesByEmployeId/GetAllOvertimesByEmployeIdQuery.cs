using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
