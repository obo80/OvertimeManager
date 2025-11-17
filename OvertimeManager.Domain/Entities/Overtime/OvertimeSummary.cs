using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Entities.Overtime
{
    public class OvertimeSummary
    {
        public int Id { get; set; }
        public double TakenOvertime { get; set; }       //time added to amount after overtime dome
        public double SettledOvertimet { get; set; }    //time paid or reveived 
        public double UnsetledOvertime { get; set; }    //current amount of overtime - added new and substracted when settled
    }
}
