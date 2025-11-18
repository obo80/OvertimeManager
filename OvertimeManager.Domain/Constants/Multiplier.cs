using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Constants
{
    public enum MultiplierType
    {
        Normal,
        Multiplied
    };
    public static class Multiplier
    {
        public static double GetMultipliedValue (MultiplierType type)
        {
            switch (type)
            {
                case MultiplierType.Normal:
                    return 1.0;
                case MultiplierType.Multiplied:
                    return 1.5;
                default:
                    return 0.0;
            }
        }
    }
}
