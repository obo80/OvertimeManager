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

        /// <summary>
        /// Add newly taken overtime to unsettled (amount of active overtime)
        /// </summary>
        /// <param name="time"></param>
        public void AddTakenOvertime(double time)
        {
            TakenOvertime += time;
            UnsettledOvertime += time;
        }

        /// <summary>
        /// Settle the overtime and move it from unsettled (amount of active overtime) to settled
        /// </summary>
        /// <param name="time"></param>
        public void SettleOvertime(double time)
        {
            SettledOvertime += time;
            UnsettledOvertime -= time;
        }
        /// <summary>
        /// To check if there is enough unsettled overtime (amount of active overtime) to settle the requested amount
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool CanSettleOvertime(double time)
        {
            return UnsettledOvertime >= time;
        }
    }
}
