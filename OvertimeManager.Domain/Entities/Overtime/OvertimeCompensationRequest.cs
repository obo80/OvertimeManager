using OvertimeManager.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Entities.Overtime
{
    public class OvertimeCompensationRequest : OvertimeRequestBase
    {
        public double Multiplier { get; private set; } 
        public double RequestedTime { get; set; }
        public double CompensatedTime { get; private set; }

        public OvertimeCompensationRequest(Constants.MultiplierType multiplierType)
        {
            Multiplier = Constants.Multiplier.GetMultipliedValue(multiplierType); ;
            CompensatedTime = CountComensatedTime(Multiplier, RequestedTime);
        }

        private double CountComensatedTime(double multiplier, double requestedTime)
        {
            var compensatedTime = requestedTime * multiplier;
            return compensatedTime;
        }
    }
}
