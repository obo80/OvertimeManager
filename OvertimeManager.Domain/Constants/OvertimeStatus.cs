using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Constants
{
    public static class OvertimeStatus
    {
        //public const string Waiting = "Waiting";
        //public const string Rejected = "Rejected";
        //public const string Approved = "Approved";
        //public const string Done = "Done";

        public static List<string> Status { get; set; } = new List<string>()
        {
            "Waiting",
            "Rejected",
            "Approved",
            "Done"
        };
    }
}
