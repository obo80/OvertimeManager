using Xunit;
using OvertimeManager.Domain.Entities.Overtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OvertimeManager.Domain.Constants;

namespace OvertimeManager.Domain.Entities.Overtime.Tests
{
    public class CompensationRequestTests
    {
        [Theory]
        [InlineData(1.0, false)]
        [InlineData(1.0, true)]
        [InlineData(1.5, false)]
        [InlineData(1.5, true)]
        [InlineData(2.0, false)]
        [InlineData(2.0, true)]
        [InlineData(2.5, false)]
        [InlineData(2.5, true)]
        [InlineData(5.0, false)]
        [InlineData(5.0, true)]
        [InlineData(7.5, false)]
        [InlineData(7.5, true)]

        public void SetCompensationTest(double requestedTime, bool isMultiplied)
        {
            //arrange
            CompensationRequest request = new CompensationRequest();


            //act
            request.RequestedTime = requestedTime;
            request.SetCompensation(isMultiplied);
            var compensatedTime = request.CompensatedTime;
            var expectedResult = requestedTime * Multiplier.GetMultipliedValue(isMultiplied);

            //assert
            Assert.Equal(expectedResult, compensatedTime);

        }
    }

}