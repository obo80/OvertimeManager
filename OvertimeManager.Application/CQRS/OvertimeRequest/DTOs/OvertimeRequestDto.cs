using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OvertimeManager.Application.CQRS.OvertimeRequest.DTOs
{
    public class OvertimeRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string BusinessJustificationReason { get; set; } = default!;
        public DateTime CreatedAt { get; set; } 
        public DateTime CreatedForDay { get; set; }

        public int RequesterdByEmployeeId { get; set; }
        //public virtual Domain.Entities.User.Employee? RequestedByEmployee { get; set; }

        public int RequesedForEmployeeId { get; set; }
        //public virtual Domain.Entities.User.Employee? RequestedForEmployee { get; set; }

        public double? ActualTime { get; set; } 
        public int ApprovalStatusId { get; set; }
        public double RequestedTime { get; set; }

    }
}
