using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.Manager.DTOs
{
    public class EmployeeOvertimeRequestsDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeEmail { get; set; } = default!;
        public IEnumerable<GetOvertimeDto> OvertimeRequest { get; set; } = new List<GetOvertimeDto>();
    }
}
