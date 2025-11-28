using OvertimeManager.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Entities.Overtime
{
    public class CompensationRequest : OvertimeRequestBase
    {
        public double Multiplier { get; private set; } 
        public double RequestedTime { get; set; }
        public double CompensatedTime { get; private set; }
        public CompensationRequest()
        {
            
        }
        /// <summary>
        /// Sets the compensation based on whether the request is created by employee or manager.
        /// Employee requests are not multiplied, while manager requests are multiplied.
        /// </summary>
        /// <param name="isMultiplied"></param>
        public void SetCompensation(bool isMultiplied)
        {
            Multiplier = Constants.Multiplier.GetMultipliedValue(isMultiplied);
            CompensatedTime = CountCompensatedTime(Multiplier, RequestedTime);
        }

        private double CountCompensatedTime(double multiplier, double requestedTime)
        {
            var compensatedTime = requestedTime * multiplier;
            return compensatedTime;
        }
    }
}
