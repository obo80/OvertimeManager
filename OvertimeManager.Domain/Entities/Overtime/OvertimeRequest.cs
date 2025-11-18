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
        public string Name { get; set; }
        public string BusinessJustificationReason { get; set; }
        public double? ActualTime { get; set; } = null;
        public int ApprovalStatusId { get; set; } = 1;
        public double RequestedTime { get; set; }
    }
}
