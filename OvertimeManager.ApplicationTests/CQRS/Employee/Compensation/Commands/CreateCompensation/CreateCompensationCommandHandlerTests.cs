using FluentAssertions;
using Moq;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;
using Xunit;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Commands.CreateCompensation.Tests
{
    public class CreateCompensationCommandHandlerTests
    {
        private readonly CreateCompensationCommandHandler _handler;
        private readonly Mock<ICompensationRepository> _compensationRepositoryMock = new();
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock = new();
        private readonly Mock<AutoMapper.IMapper> _mapperMock = new();

        private CreateCompensationCommand _command;
        private CompensationRequest _compensationRequest;
        private Domain.Entities.User.Employee _employee;
        private const int employeeId = 1;     //default employee id

        public CreateCompensationCommandHandlerTests()
        {
            _handler = new CreateCompensationCommandHandler(
                _compensationRepositoryMock.Object, _employeeRepositoryMock.Object, _mapperMock.Object);
            _command = new CreateCompensationCommand(employeeId)
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
            _compensationRequest = new CompensationRequest()
            {
                Id = 1,
                RequestedByEmployeeId = employeeId,
                RequestedForEmployeeId = employeeId,
                RequestedTime = 2,
            };
        }
        [Fact()]
        public async Task Handle_ValidData_ReturnsIdAsync()
        {
            //arrange
            _compensationRequest.RequestedTime = 2;
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(_command.RequestedForEmployeeId)).ReturnsAsync(_employee);
            _mapperMock.Setup(m => m.Map<CompensationRequest>(_command)).Returns(_compensationRequest);
            _compensationRepositoryMock.Setup(repo => repo.CreateCompensationAsync(_compensationRequest)).ReturnsAsync(_compensationRequest.Id);

            //act
            var result = await _handler.Handle(_command, CancellationToken.None);

            //assert
            result.Should().Be(1);
        }


        [Fact()]
        public async Task Handle_MissingTimeForCompansation_ThrowsBadRequestException()
        {
            //arrange
            _compensationRequest.RequestedTime = 7;     //more than unsettled overtime for getting BadRequestException
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(_command.RequestedForEmployeeId)).ReturnsAsync(_employee);
            _mapperMock.Setup(m => m.Map<CompensationRequest>(_command)).Returns(_compensationRequest);
            _compensationRepositoryMock.Setup(repo => repo.CreateCompensationAsync(_compensationRequest)).ReturnsAsync(_compensationRequest.Id);

            //act
            Func<Task> result = async () => await _handler.Handle(_command, CancellationToken.None);

            //assert
            await result.Should().ThrowAsync<BadRequestException>()
                .WithMessage("Insufficient unsettled overtime to settle the requested time.");
        }

        [Fact()]
        public async Task Handle_InvalidEployee_ThrowsNotFoundException()
        {
            //arrange
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(_command.Id))
                .ReturnsAsync((Domain.Entities.User.Employee)null!);

            //act
            Func<Task> result = async () => await _handler.Handle(_command, CancellationToken.None);

            //assert
            await result.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Employee with id: 1 doesn't exist");
        }
    }
}