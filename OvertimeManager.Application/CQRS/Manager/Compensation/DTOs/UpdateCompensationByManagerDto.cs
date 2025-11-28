using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.DTOs
{
    public class UpdateCompensationByManagerDto
    {
        public int RequestedForEmployeeId { get; set; }
        public double RequestedTime { get; set; }
    }
}
