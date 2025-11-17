using OvertimeManager.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Entities.Overtime
{
    public class OvertimeRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Reason { get; set; }

        public int RequesterdByEmployeeId { get; set; }
        public virtual Employee? RequestedByEmployee { get; set; }


        public int RequesedForEmployeeId { get; set; }
        public virtual Employee? RequestedForEmployee { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime CreatedForDay { get; set; } 

        public double RequestedTime { get; set; }
        public double? ActualTime { get; set; } = null;

        public int ApprovalStatusId { get; set; } = 1;

    }
}
