using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.OvertimeCompensationRequest.DTOs
{
    public class OvertimeCompensationRequestDto
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateOnly CreatedForDay { get; set; }

        public int RequesterdByEmployeeId { get; set; }
        //public virtual Domain.Entities.User.Employee? RequestedByEmployee { get; set; }

        public int RequesedForEmployeeId { get; set; }
        //public virtual Domain.Entities.User.Employee? RequestedForEmployee { get; set; }

        public double Multiplier { get; set; }
        public double RequestedTime { get; set; }
        public double CompensatedTime { get; set; }
    }
}
