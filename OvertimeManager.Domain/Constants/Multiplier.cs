using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Constants
{
    public static class Multiplier
    {
        const double Normal = 1.0;
        const double Multiplied = 1.5;
        public static double GetMultipliedValue (bool isMultiplied)
        {
            if (isMultiplied) 
                return Multiplied;

            return Normal;
        }
    }
}
