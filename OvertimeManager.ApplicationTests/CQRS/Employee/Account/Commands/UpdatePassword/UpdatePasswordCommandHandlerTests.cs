using FluentAssertions;
using Microsoft.AspNet.Identity;
using Moq;
using OvertimeManager.Application.Common;
using OvertimeManager.Application.CQRS.Employee.Account.Commands.SetPassword;
using OvertimeManager.Domain.Entities.User;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;
using Xunit;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.UpdatePassword.Tests
{
    public class UpdatePasswordCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock = new();
        private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
        private readonly Mock<IJwtService> _jwtServiceMock = new();
        private readonly UpdatePasswordCommandHandler _handler;

        private Domain.Entities.User.Employee _employee;
        private UpdatePasswordCommand _command;

        public UpdatePasswordCommandHandlerTests()
        {
            _handler = new UpdatePasswordCommandHandler(_employeeRepositoryMock.Object, _passwordHasherMock.Object, _jwtServiceMock.Object);

            _employee = new Domain.Entities.User.Employee()
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "Jan.Kowalski@test.com",
                PasswordHash = "hashedPassword123",
                MustChangePassword = false
            };

            _command = new UpdatePasswordCommand()
            {
                Email = "Jan.Kowalski@test.com",
                CurrentPassword = "oldPassword123",
                NewPassword = "password123",
                ConfirmedPassword = "password123"
            };
        }

        [Fact()]
        public async Task Handle_ValidCredential_ReturnsTokenAsync()
        {
            _employeeRepositoryMock.Setup(repo => repo.GetByEmailAsync(_command.Email)).ReturnsAsync(_employee);
            _jwtServiceMock.Setup(jwt => jwt.GenerateJwtToken(_employee)).Returns("new_jwt_token");
            _passwordHasherMock.Setup(hasher => hasher.VerifyHashedPassword(_employee.PasswordHash!, _command.CurrentPassword))
                .Returns(PasswordVerificationResult.Success);

            //act
            var result = await _handler.Handle(_command, CancellationToken.None);

            //assert
            result.Should().Be("new_jwt_token");
        }

        [Fact()]
        public async Task Handle_ValidCredential_SetEmployeePropertiesCorrectlyAsync()
        {
            _employeeRepositoryMock.Setup(repo => repo.GetByEmailAsync(_command.Email!))
                .ReturnsAsync(_employee);

            _jwtServiceMock.Setup(jwt => jwt.GenerateJwtToken(_employee))
                .Returns("new_jwt_token");

            _passwordHasherMock.Setup(hasher => hasher.VerifyHashedPassword(_employee.PasswordHash!, _command.CurrentPassword))
                .Returns(PasswordVerificationResult.Success);

            _passwordHasherMock.Setup(hasher => hasher.HashPassword(_command.NewPassword!))
                .Returns("hashedUpdatedPassword123");

            //act
            var result = await _handler.Handle(_command, CancellationToken.None);

            //assert
            _employee.MustChangePassword.Should().BeFalse();
            _employee.PasswordHash.Should().NotBeNullOrEmpty();
            _employee.PasswordHash.Should().Be("hashedUpdatedPassword123");
        }


        [Fact()]
        public async Task Handle_InValidCurrentPassword_ThrowsForbidException()
        {
            //arrange
            _employeeRepositoryMock.Setup(repo => repo.GetByEmailAsync(_command.Email!))
                .ReturnsAsync(_employee);
            _passwordHasherMock.Setup(hasher => hasher.VerifyHashedPassword(_employee.PasswordHash!, _command.CurrentPassword))
                .Returns(PasswordVerificationResult.Failed);


            //act
            Func<Task> result = async () => await _handler.Handle(_command, CancellationToken.None);

            //assert
            await result.Should().ThrowAsync<ForbidException>()
                .WithMessage("Current password is incorrect.");
        }

        [Fact()]
        public async Task Handle_CurrentPasswordDoesntSetYet_ThrowsForbidException()
        {
            //arrange
            _employee.MustChangePassword = true;

            _employeeRepositoryMock.Setup(repo => repo.GetByEmailAsync(_command.Email!))
                .ReturnsAsync(_employee);

            //act
            Func<Task> result = async () => await _handler.Handle(_command, CancellationToken.None);

            //assert
            await result.Should().ThrowAsync<ForbidException>()
                .WithMessage("You must set a new password before updating it.");
        }

        [Fact()]
        public async Task Handle_InValidEmail_ThrowsNotFoundException()             //employee not found in database
        {
            //arrange

            _employeeRepositoryMock.Setup(repo => repo.GetByEmailAsync(_command.Email!))
                .ReturnsAsync((Domain.Entities.User.Employee?)null);    //email not found

            //act
            Func<Task> result = async () => await _handler.Handle(_command, CancellationToken.None);
            //assert
            await result.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Employee not found for given email: Jan.Kowalski@test.com");
        }
    }
}