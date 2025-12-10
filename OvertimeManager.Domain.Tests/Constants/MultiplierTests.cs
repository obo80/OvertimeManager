using Xunit;
using OvertimeManager.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Constants.Tests
{
    public class MultiplierTests
    {
        [Theory]
        [InlineData(1.0, false)]
        [InlineData(1.5, true)]
        public void GetMultipliedValueTest(double value, bool isTrue)
        {

            var isMultiplied = Domain.Constants.Multiplier.GetMultipliedValue(isTrue);

            Assert.Equal(value, isMultiplied);
        }
    }
}