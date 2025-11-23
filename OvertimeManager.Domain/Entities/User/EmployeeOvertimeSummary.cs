using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Entities.User
{
    public class EmployeeOvertimeSummary
    {
        public int Id { get; set; }
        public double TakenOvertime { get; set; } = 0;      //time added to amount after overtime dome
        public double SettledOvertime { get; set; } = 0;   //time paid or reveived 
        public double UnsettledOvertime { get; set; } = 0;   //current amount of overtime - added new and substracted when settled
    }
}
