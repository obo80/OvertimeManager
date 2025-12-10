using Xunit;
using OvertimeManager.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Entities.User.Tests
{
    public class EmployeeOvertimeSummaryTests
    {
        [Theory()]
        [InlineData(1.0, 3.0, 0.0)]
        [InlineData(2.0, 1.0, 12.0)]
        [InlineData(3.0, 3.5, 14.5)]
        [InlineData(1.5, 5.0, 11.25)]
        [InlineData(2.5, 0.0, 0.0)]
        [InlineData(5.0, 5.0, 10.0)]
        public void AddTakenOvertimeTest(double requestedTime, double takenOvertime, double unsettledOvertime)
        {
            //arrange
            EmployeeOvertimeSummary employeeSummary = new EmployeeOvertimeSummary();
            employeeSummary.UnsettledOvertime = unsettledOvertime;
            employeeSummary.TakenOvertime = takenOvertime;
            //act
            employeeSummary.AddTakenOvertime(requestedTime);

            //assert
            Assert.Equal(employeeSummary.TakenOvertime, takenOvertime + requestedTime);
            Assert.Equal(employeeSummary.UnsettledOvertime, unsettledOvertime + requestedTime);
        }

        [Theory()]
        [InlineData(1.0, 3.0, 0.0)]
        [InlineData(2.0, 1.0, 12.0)]
        [InlineData(3.0, 3.5, 14.5)]
        [InlineData(1.5, 5.0, 11.25)]
        [InlineData(2.5, 0.0, 0.0)]
        [InlineData(5.0, 5.0, 10.0)]
        public void SettleOvertimeTest(double requestedTime, double settledOvertime, double unsettledOvertime)
        {
            //arrange
            EmployeeOvertimeSummary employeeSummary = new EmployeeOvertimeSummary();
            employeeSummary.UnsettledOvertime = unsettledOvertime;
            employeeSummary.SettledOvertime = settledOvertime;

            //act
            employeeSummary.SettleOvertime(requestedTime);

            //assert
            Assert.Equal(employeeSummary.SettledOvertime, settledOvertime + requestedTime);
            Assert.Equal(employeeSummary.UnsettledOvertime, unsettledOvertime - requestedTime);
        }


        [Theory()]
        [InlineData(1.0, 3.0)]
        [InlineData(1.0, 1.0)]
        [InlineData(3.0, 3.5)]
        public void CanSettleOvertimeTrueTest(double requestedTime, double unsettledOvertime)
        {
            //arrange
            EmployeeOvertimeSummary employeeSummary = new EmployeeOvertimeSummary();
            employeeSummary.UnsettledOvertime = unsettledOvertime;
            //act
            var result = employeeSummary.CanSettleOvertime(requestedTime);

            //assert
            Assert.True(result);

        }

        [Theory()]
        [InlineData(1.0, 0.0)]
        [InlineData(1.5, 1.0)]
        [InlineData(3.5, 0.5)]
        public void CanSettleOvertimeFalseTest(double requestedTime, double unsettledOvertime)
        {
            //arrange
            EmployeeOvertimeSummary employeeSummary = new EmployeeOvertimeSummary();
            employeeSummary.UnsettledOvertime = unsettledOvertime;
            //act
            var result = employeeSummary.CanSettleOvertime(requestedTime);

            //assert
            Assert.False(result);

        }
    }
}