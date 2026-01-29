namespace OvertimeManager.Domain.Constants.Tests
{
    public class MultiplierTests
    {
        [Theory]
        [InlineData(1.0, false)]
        [InlineData(1.5, true)]
        public void GetMultipliedValueTest(double value, bool isTrue)
        {

            var isMultiplied = Multiplier.GetMultipliedValue(isTrue);

            Assert.Equal(value, isMultiplied);
        }
    }
}