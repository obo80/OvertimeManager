using FluentAssertions;
using Moq;
using OvertimeManager.Application.CQRS.Employee.Compensation.Commands.CancelCompensation;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;
using Xunit;

namespace OvertimeManager.ApplicationTests.CQRS.Employee.Compensation.Commands.CancelCompensation
{
    public class CancelCompensationCommandHandlerTests
    {
        private readonly CancelCompensationCommandHandler _handler;

        private readonly Mock<ICompensationRepository> _compensationRepositoryMock = new ();
        private CancelCompensationCommand _command;
        private CompensationRequest _compensationRequest;
        private const int employeeId = 1;     //default employee id
        private const int requestId = 1;      //default request id

        public CancelCompensationCommandHandlerTests()
        {
            _handler = new CancelCompensationCommandHandler(_compensationRepositoryMock.Object);
            _command = new CancelCompensationCommand(employeeId, requestId);
            _compensationRequest = new CompensationRequest
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
            _compensationRepositoryMock.Setup(repo => repo.GetByIdAsync(_command.CompensationId))
                .ReturnsAsync(_compensationRequest);

            //act
            await _handler.Handle(_command, CancellationToken.None);

            //assert
            _compensationRequest.Status.Should().Be("Cancelled");

        }

        [Fact()]
        public async Task Handle_InvalidRequest_ThrowsNotFoundExceptionAsync()
        {
            //arrange
            _compensationRepositoryMock.Setup(repo => repo.GetByIdAsync(_command.CompensationId))
                .ReturnsAsync((CompensationRequest)null!);

            //act
            Func<Task> result = async () => await _handler.Handle(_command, CancellationToken.None);

            //assert
            await result.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Compensation request with id: 1 doesn't exist");

        }

        [Fact()]
        public async Task Handle_InvalidEmployee_ThrowsForbidExceptionAsync()
        {
            //arrange
            var incorrectEmployeeId = 2; //change employee id to simulate invalid employee
            _command = new CancelCompensationCommand(incorrectEmployeeId, requestId);
            _compensationRepositoryMock.Setup(repo => repo.GetByIdAsync(_command.CompensationId))
                .ReturnsAsync(_compensationRequest);

            //act
            Func<Task> result = async () => await _handler.Handle(_command, CancellationToken.None);

            //assert
            await result.Should().ThrowAsync<ForbidException>()
                .WithMessage("You are not authorized to update this compensation request.");
        }
    }
}