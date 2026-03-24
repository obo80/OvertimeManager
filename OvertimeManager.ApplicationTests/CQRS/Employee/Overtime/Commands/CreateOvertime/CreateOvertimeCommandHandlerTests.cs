using FluentAssertions;
using Moq;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;
using Xunit;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Commands.CreateOvertime.Tests
{
    public class CreateOvertimeCommandHandlerTests
    {
        private readonly CreateOvertimeCommandHandler _handler;
        private readonly Mock<IOvertimeRepository> _overtimeRepositoryMock = new();
        private readonly Mock<AutoMapper.IMapper> _mapperMock = new();

        private CreateOvertimeCommand _command;
        private OvertimeRequest _overtimeRequest;

        private Domain.Entities.User.Employee _employee;
        private const int employeeId = 1;     //default employee id

        public CreateOvertimeCommandHandlerTests()
        {
            _handler = new CreateOvertimeCommandHandler(_overtimeRepositoryMock.Object, _mapperMock.Object);
            _command = new CreateOvertimeCommand(employeeId)
            {
                Id = 1,
                RequestedTime = 2
            };

            _employee = new Domain.Entities.User.Employee()
            {
                Id = employeeId,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "Jan.Kowalski@test.com",
                OvertimeSummary = new Domain.Entities.User.EmployeeOvertimeSummary()
                {
                    Id = 1,
                    UnsettledOvertime = 5
                }
            };

            _overtimeRequest = new OvertimeRequest()
            {
                Id = 1,
                RequestedByEmployeeId = employeeId,
                RequestedForEmployeeId = employeeId,
                RequestedTime = 2
            };
        }


        [Fact()]
        public async Task Handle_ValidData_ReturnsIdAsync()
        {
            //arrange
            _mapperMock.Setup(m => m.Map<OvertimeRequest>(_command)).Returns(_overtimeRequest);
            _overtimeRepositoryMock.Setup(repo => repo.CreateOvertimeAsync(_overtimeRequest)).ReturnsAsync(_overtimeRequest.Id);

            //act
            var result = await _handler.Handle(_command, CancellationToken.None);

            //assert
            result.Should().Be(1);
        }
    }
}