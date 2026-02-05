using FluentAssertions;
using Microsoft.AspNet.Identity;
using Moq;
using OvertimeManager.Application.Common;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;
using Xunit;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.SetPassword.Tests
{
    public class SetPasswordCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock = new();
        private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
        private readonly Mock<IJwtService> _jwtServiceMock = new();
        private readonly SetPasswordCommandHandler _handler;

        private Domain.Entities.User.Employee _employee;
        private SetPasswordCommand _command;

        public SetPasswordCommandHandlerTests()
        {
            _handler = new SetPasswordCommandHandler(_employeeRepositoryMock.Object, _passwordHasherMock.Object, _jwtServiceMock.Object);

            _employee = new Domain.Entities.User.Employee()
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "Jan.Kowalski@test.com",
                MustChangePassword = true
            };

            _command = new SetPasswordCommand()
            {
                Email = "Jan.Kowalski@test.com",
                NewPassword = "password123",
                ConfirmedPassword = "password123"
            };
        }

        [Fact()]
        public async Task Handle_ValidCredential_ReturnsTokenAsync()
        {

            //arrange
            _employeeRepositoryMock.Setup(repo => repo.GetByEmailAsync(_command.Email!)).ReturnsAsync(_employee);
            _jwtServiceMock.Setup(jwt => jwt.GenerateJwtToken(_employee)).Returns("new_jwt_token");

            //act
            var result = await _handler.Handle(_command, CancellationToken.None);

            //assert
            result.Should().Be("new_jwt_token");
        }

        [Fact()]
        public async Task Handle_ValidCredential_SetEmployeePropertiesCorrectlyAsync()
        {
            //arrange
            _employeeRepositoryMock.Setup(repo => repo.GetByEmailAsync(_command.Email!)).ReturnsAsync(_employee);
            _jwtServiceMock.Setup(jwt => jwt.GenerateJwtToken(_employee)).Returns("new_jwt_token");
            _passwordHasherMock.Setup(hasher => hasher.HashPassword(_command.NewPassword!)).Returns("hashedPassword123");

            //act
            var result = await _handler.Handle(_command, CancellationToken.None);

            //assert
            _employee.MustChangePassword.Should().BeFalse();
            _employee.PasswordHash.Should().NotBeNullOrEmpty();
            _employee.PasswordHash.Should().Be("hashedPassword123");
        }

        [Fact()]
        public async Task Handle_ExistingPassowrd_ThrowsForbidException()
        {
            //arrange
            _employee.MustChangePassword = false; //password change not required

            _employeeRepositoryMock.Setup(repo => repo.GetByEmailAsync(_command.Email!)).ReturnsAsync(_employee);
            _jwtServiceMock.Setup(jwt => jwt.GenerateJwtToken(_employee)).Returns("new_jwt_token");
            //act
            Func<Task> result = async () => await _handler.Handle(_command, CancellationToken.None);

            //assert
            await result.Should().ThrowAsync<ForbidException>()
                .WithMessage("Password is already set for this user.");
        }

        [Fact()]
        public async Task Handle_InValidEmail_ThrowsNotFoundExceptionAsync()     //employee not found in database
        {
            //arrange 
            _employeeRepositoryMock.Setup(repo => repo.GetByEmailAsync(_command.Email!))
                .ReturnsAsync((Domain.Entities.User.Employee?)null);        //email not found

            //act
            Func<Task> result = async () => await _handler.Handle(_command, CancellationToken.None);
            //assert
            await result.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"User not found for given email: Jan.Kowalski@test.com");
        }
    }
}