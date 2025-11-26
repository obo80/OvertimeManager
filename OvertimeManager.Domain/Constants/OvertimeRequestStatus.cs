using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Constants
{
    public enum StatusEnum
    {
        Pending = 1,
        Cancelled = 2,
        Approved = 3,
        Rejected = 4,
        Done = 5
    }
    public static class OvertimeRequestStatus
    {
        //public const string Pending = "Pending";
        //public const string Cancelled = "Cancelled";
        //public const string Approved = "Approved";
        //public const string Rejected = "Rejected";
        //public const string Done = "Done";
        public static StatusEnum Status { get; set; }

        public static List<string> Statuses { get; set; } = new List<string>()
        {
            "Pending",
            "Cancelled",
            "Approved",
            "Rejected",
            "Done"
        };
    }
}
