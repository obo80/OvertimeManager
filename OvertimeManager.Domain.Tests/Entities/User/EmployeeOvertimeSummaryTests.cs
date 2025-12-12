using Xunit;
using OvertimeManager.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Tests.Entities.User
{
    public class EmployeeOvertimeSummaryTests
    {
        [Theory()]
        [InlineData(1.0, 3.0, 0.0, 4.0, 1.0)]
        [InlineData(2.0, 1.0, 12.0, 3.0, 14.0)]
        [InlineData(3.0, 3.5, 14.5, 6.5, 17.5)]
        [InlineData(1.5, 5.0, 11.25, 6.5, 12.75)]
        [InlineData(2.5, 0.0, 0.0, 2.5, 2.5)]
        [InlineData(5.0, 5.0, 10.0, 10.0, 15.0)]
        public void AddTakenOvertimeTest(double requestedTime, double initialTakenOvertime, double initialUnsettledOvertime,
            double outputTakenOvertime, double outputUnsettledOvertime)
        {
            //arrange
            var employeeSummary = new EmployeeOvertimeSummary
            {
                UnsettledOvertime = initialUnsettledOvertime,
                TakenOvertime = initialTakenOvertime
            };
            //act
            employeeSummary.AddTakenOvertime(requestedTime);

            //assert
            Assert.Equal(employeeSummary.TakenOvertime, outputTakenOvertime);
            Assert.Equal(employeeSummary.UnsettledOvertime, outputUnsettledOvertime);
        }

        [Theory()]
        [InlineData(1.0, 3.0, 4.0, 4.0, 3.0)]
        [InlineData(2.0, 1.0, 12.0, 3.0, 10.0)]
        [InlineData(3.0, 3.5, 14.5, 6.5, 11.5)]
        [InlineData(1.5, 5.0, 11.25, 6.5, 9.75)]
        [InlineData(5.0, 5.0, 10.0, 10.0, 5.0)]
        public void SettleOvertimeTest(double requestedTime, double initialSettledOvertime, double initialUnsettledOvertime,
            double outputSettledOvertime, double outputlUnsettledOvertime)
        {
            //arrange
            var employeeSummary = new EmployeeOvertimeSummary
            {
                UnsettledOvertime = initialUnsettledOvertime,
                SettledOvertime = initialSettledOvertime
            };

            //act
            employeeSummary.SettleOvertime(requestedTime);

            //assert
            Assert.Equal(employeeSummary.SettledOvertime, outputSettledOvertime);
            Assert.Equal(employeeSummary.UnsettledOvertime, outputlUnsettledOvertime);
        }


        [Theory()]
        [InlineData(1.0, 3.0)]
        [InlineData(1.0, 1.0)]
        [InlineData(3.0, 3.5)]
        public void CanSettleOvertime_ReturnTrueTest(double requestedTime, double unsettledOvertime)
        {
            //arrange
            var employeeSummary = new EmployeeOvertimeSummary
            {
                UnsettledOvertime = unsettledOvertime
            };
            //act
            var result = employeeSummary.CanSettleOvertime(requestedTime);

            //assert
            Assert.True(result);

        }

        [Theory()]
        [InlineData(1.0, 0.0)]
        [InlineData(1.5, 1.0)]
        [InlineData(3.5, 0.5)]
        public void CanSettleOvertime_ReturnFalseTest(double requestedTime, double unsettledOvertime)
        {
            //arrange
            var employeeSummary = new EmployeeOvertimeSummary
            {
                UnsettledOvertime = unsettledOvertime
            };
            //act
            var result = employeeSummary.CanSettleOvertime(requestedTime);

            //assert
            Assert.False(result);
        }
    }
}