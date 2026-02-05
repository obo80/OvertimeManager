using FluentAssertions;
using OvertimeManager.Domain.Entities.Overtime;

namespace OvertimeManager.Domain.Tests.Entities.Overtime
{
    public class CompensationRequestTests
    {
        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(1.0, 1.0)]
        [InlineData(1.5, 1.5)]
        [InlineData(2.0, 2.0)]
        [InlineData(7.5, 7.5)]
        public void SetCompensation_Return_NormalCompensationTime(double requestedTime, double expectedResult)
        {
            //arrange
            const bool isMultiplied = false;
            var request = new CompensationRequest
            {
                RequestedTime = requestedTime
            };

            //act
            request.SetCompensation(isMultiplied);
            var compensatedTime = request.CompensatedTime;

            //assert
            compensatedTime.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(1.0, 1.5)]
        [InlineData(1.5, 2.25)]
        [InlineData(2.0, 3.0)]
        [InlineData(7.5, 11.25)]
        public void SetCompensation_Return_MultipliedCompensationTime(double requestedTime, double expectedResult)
        {
            //arrange
            const bool isMultiplied = true;
            var request = new CompensationRequest
            {
                RequestedTime = requestedTime
            };

            //act
            request.SetCompensation(isMultiplied);
            var compensatedTime = request.CompensatedTime;

            //assert
            compensatedTime.Should().Be(expectedResult);
        }
    }

}