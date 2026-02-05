using FluentAssertions;
using Moq;
using OvertimeManager.Application.CQRS.Employee.Compensation.Commands.SetCompensationDone;
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

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Commands.SetCompensationDone.Tests
{
    public class SetCompensationDoneCommandHandlerTests
    {
        private readonly Mock<ICompensationRepository> _compensationRepositoryMock = new();
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock = new();
        private readonly SetCompensationDoneCommandHandler _handler;

        private SetCompensationDoneCommand _command;
        private CompensationRequest _compensationRequest;

        private Domain.Entities.User.Employee _employee;
        private const int employeeId = 1;     //default employee id
        private const int requestId = 1;      //default request id

        public SetCompensationDoneCommandHandlerTests()
        {
            _handler = new SetCompensationDoneCommandHandler(_compensationRepositoryMock.Object,
                _employeeRepositoryMock.Object);
            _command = new SetCompensationDoneCommand(employeeId, requestId);
            _compensationRequest = new CompensationRequest
            {
                Id = requestId,
                RequestedForEmployeeId = employeeId,
                Status = "Approved",         //correct status
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

        }
        [Fact()]
        public async Task Handle_ValidRequest_SetStatusToDoneledAsync()
        {
            //arrange
            _compensationRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(_compensationRequest);
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employeeId))
                .ReturnsAsync(_employee);


            //act
            await _handler.Handle(_command, CancellationToken.None);

            //assert
            _compensationRequest.Status.Should().Be("Done");
        }
        [Fact()]
        public async Task Handle_ValidRequest_SetOvertimeSummmaryValueCorrectlydAsync()
        {
            //arrange
            _compensationRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(_compensationRequest);
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employeeId))
                .ReturnsAsync(_employee);

            //act
            await _handler.Handle(_command, CancellationToken.None);

            //assert
            _employee.OvertimeSummary!.UnsettledOvertime.Should().Be(3);
            _compensationRequest.Status.Should().Be("Done");
        }


        [Fact()]
        public async Task Handle_OtherEmployeeRequest_ThrowsForbidExceptionAsync()
        {
            //arrange
            
            _compensationRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(_compensationRequest);
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employeeId))
                .ReturnsAsync(_employee);

            _command = new SetCompensationDoneCommand(employeeId + 1, requestId); //different employee id
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

        [InlineData("Pending")]
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
                .WithMessage("Only approved compensation requests can be done.");
        }


    }

}