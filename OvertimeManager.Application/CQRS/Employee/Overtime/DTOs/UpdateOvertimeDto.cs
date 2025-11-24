using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.DTOs
{
    public class UpdateOvertimeDto
    {
        public string? Name { get; set; } = default;
        public string? BusinessJustificationReason { get; set; } = default;
        public double? RequestedTime { get; set; }
    }
}
