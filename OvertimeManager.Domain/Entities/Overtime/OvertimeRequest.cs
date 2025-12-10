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

        //actual time worked will be set after the overtime is completed
        public double? ActualTime { get; set; } = null;
    }
}
