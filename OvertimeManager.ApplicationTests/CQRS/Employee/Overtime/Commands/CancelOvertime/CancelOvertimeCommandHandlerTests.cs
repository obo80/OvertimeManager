using FluentAssertions;
using Moq;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;
using Xunit;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Commands.CancelOvertime.Tests
{
    public class CancelOvertimeCommandHandlerTests
    {
        private readonly CancelOvertimeCommandHandler _handler;
        private readonly Mock<IOvertimeRepository> _overtimeRepositoryMock = new();

        private CancelOvertimeCommand _command;
        private OvertimeRequest _overtimeRequest;

        private const int employeeId = 1;     //default employee id
        private const int requestId = 1;      //default request id

        public CancelOvertimeCommandHandlerTests()
        {
            _handler = new CancelOvertimeCommandHandler(_overtimeRepositoryMock.Object);
            _command = new CancelOvertimeCommand(employeeId, requestId);
            _overtimeRequest = new OvertimeRequest()
            {
                Id = requestId,
                RequestedForEmployeeId = employeeId,
                Status = "Pending"
            };
        }


        [Fact()]
        public async Task Handle_ValidRequest_SetStatusToCancelledAsync()
        {
            //arrange
            _overtimeRepositoryMock.Setup(repo => repo.GetByIdAsync(_command.OvertimeId))
                .ReturnsAsync(_overtimeRequest);

            //act
            await _handler.Handle(_command, CancellationToken.None);

            //assert
            _overtimeRequest.Status.Should().Be("Cancelled");

        }

        [Fact()]
        public async Task Handle_InvalidRequest_ThrowsNotFoundExceptionAsync()
        {
            //arrange
            _overtimeRepositoryMock.Setup(repo => repo.GetByIdAsync(_command.OvertimeId))
                .ReturnsAsync((OvertimeRequest)null!);

            //act
            Func<Task> result = async () => await _handler.Handle(_command, CancellationToken.None);

            //assert
            await result.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Overtime request with id: 1 doesn't exist");

        }

        [Fact()]
        public async Task Handle_InvalidEmployee_ThrowsForbidExceptionAsync()
        {
            //arrange
            var invalidEmployeeId = 2;
            var command = new CancelOvertimeCommand(invalidEmployeeId, requestId);
            _overtimeRepositoryMock.Setup(repo => repo.GetByIdAsync(command.OvertimeId))
                .ReturnsAsync(_overtimeRequest);

            //act
            Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

            //assert
            await result.Should().ThrowAsync<ForbidException>()
                .WithMessage("You are not authorized to update this overtime request.");
        }
    }
}