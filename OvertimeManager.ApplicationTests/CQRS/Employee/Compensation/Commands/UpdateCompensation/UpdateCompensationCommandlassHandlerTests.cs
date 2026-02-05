using FluentAssertions;
using Moq;
using OvertimeManager.Application.CQRS.Employee.Compensation.Commands.CreateCompensation;
using OvertimeManager.Application.CQRS.Employee.Compensation.Commands.SetCompensationDone;
using OvertimeManager.Application.CQRS.Employee.Compensation.Commands.UpdateCompensation;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Entities.User;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Commands.UpdateCompensation.Tests
{
    public class UpdateCompensationCommandlassHandlerTests
    {
        private readonly UpdateCompensationCommandlassHandler _handler;
        private readonly Mock<ICompensationRepository> _compensationRepositoryMock = new();
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock = new();

        private UpdateCompensationCommand _command;
        private Domain.Entities.User.Employee _employee;
        private CompensationRequest _compensationRequest;

        private const int employeeId = 1;
        private const int requestId = 1;
        public UpdateCompensationCommandlassHandlerTests()
        {
            _handler = new UpdateCompensationCommandlassHandler(_compensationRepositoryMock.Object, _employeeRepositoryMock.Object);
            _command = new UpdateCompensationCommand(employeeId, requestId)
            {
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

            _compensationRequest = new CompensationRequest()
            {
                Id = 1,
                RequestedByEmployeeId = employeeId,
                RequestedForEmployeeId = employeeId,
                RequestedTime = 3,
                Status = "Pending"
            };
        }

        [Fact()]
        public async Task Handle_ValidRequest_UpdatesCompensationAsync() //correct
        {
            //arrange
            _compensationRepositoryMock.Setup(repo => repo.GetByIdAsync(_command.CompensationId)).ReturnsAsync(_compensationRequest);
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(_command.CurrentEmployeeId)).ReturnsAsync(_employee);

            //act
            await _handler.Handle(_command, CancellationToken.None);

            //assert
            _compensationRequest.RequestedTime.Should().Be(_command.RequestedTime);
        }

        [Fact()]

        public async Task Handle_MissingTimeForCompansation_ThrowsBadRequestException()
        {
            //arrange
            _command.RequestedTime = 7;     //more than unsettled overtime for getting BadRequestException

            _compensationRepositoryMock.Setup(repo => repo.GetByIdAsync(_command.CompensationId)).ReturnsAsync(_compensationRequest);
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(_command.CurrentEmployeeId)).ReturnsAsync(_employee);

            //act
            Func<Task> result = async () => await _handler.Handle(_command, CancellationToken.None);

            //assert
            await result.Should().ThrowAsync<BadRequestException>()
                .WithMessage("Insufficient unsettled overtime to settle the requested time.");
        }

        [Fact()]
        public async Task Handle_OtherEmployeeRequest_ThrowsForbidExceptionAsync()
        {
            //arrange

            _compensationRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(_compensationRequest);
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employeeId))
                .ReturnsAsync(_employee);

            _command = new UpdateCompensationCommand(employeeId + 1, requestId); //different employee id
            //act
            Func<Task> result = async () => await _handler.Handle(_command, CancellationToken.None);

            //assert
            await result.Should().ThrowAsync<ForbidException>()
                .WithMessage("You are not authorized to update this compensation request.");
        }

        [Fact()]
        public async Task Handle_CurrentEmployeeDoesntExist_ThrowsForbidExceptionAsync()
        {
            //arrange
            _compensationRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(_compensationRequest);
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employeeId))
                .ReturnsAsync((Domain.Entities.User.Employee)null!);

            //act
            Func<Task> result = async () => await _handler.Handle(_command, CancellationToken.None);

            //assert
            await result.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Employee with id: 1 doesn't exist");
        }

        [Fact()]
        public async Task Handle_InvalidRequest_ThrowsNotFoundExceptionAsync()
        {
            //arrange
            _compensationRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync((CompensationRequest)null!);
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employeeId))
                .ReturnsAsync(_employee);

            //act
            Func<Task> result = async () => await _handler.Handle(_command, CancellationToken.None);

            //assert
            await result.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Compensation request with id: 1 doesn't exist");
        }

        [InlineData("Approved")]
        [InlineData("Rejected")]
        [InlineData("Cancelled")]
        [InlineData("Done")]
        [Theory()]
        public async Task Handle_InvalidRequestStatus_ThrowsBadRequestExceptionAsync(string status)
        {
            //arrange
            _compensationRequest.Status = status; //invalid status
            _compensationRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(_compensationRequest);
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employeeId))
                .ReturnsAsync(_employee);

            //act
            Func<Task> result = async () => await _handler.Handle(_command, CancellationToken.None);

            //assert
            await result.Should().ThrowAsync<BadRequestException>()
                .WithMessage("Only pending compensation requests can be updated.");
        }

    }
}