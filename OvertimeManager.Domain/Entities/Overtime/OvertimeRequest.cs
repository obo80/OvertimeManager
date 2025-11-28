using OvertimeManager.Domain.Constants;
using OvertimeManager.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Entities.Overtime
{
    public class OvertimeRequest : OvertimeRequestBase
    {
        //request details
        public string Name { get; set; } = default!;   
        public string BusinessJustificationReason { get; set; } = default!;
        public double RequestedTime { get; set; }

        //approval status is "Pending" by default on creation, approval process will update these fields

        //public string Status { get; set; } = ((StatusEnum)StatusEnum.Pending).ToString();
        //public DateTime? ApprovedAt { get; set; } = null;
        //public int? ApprovedByEmployeeId { get; set; } = null;
        //public virtual Employee? ApprovedByEmployee { get; set; }

        //actual time worked will be set after the overtime is completed
        public double? ActualTime { get; set; } = null;

        

    }
}
