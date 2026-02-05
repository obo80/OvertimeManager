using FluentAssertions;
using Microsoft.AspNet.Identity;
using Moq;
using OvertimeManager.Application.Common;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;
using Xunit;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.Login.Tests
{
    public class LoginCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock = new();
        private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
        private readonly Mock<IJwtService> _jwtServiceMock = new();
        private readonly LoginCommandHandler _handler;

        public LoginCommandHandlerTests()
        {
            _handler = new LoginCommandHandler(_employeeRepositoryMock.Object, _passwordHasherMock.Object, _jwtServiceMock.Object);
        }

        [Fact()]
        public async Task Handle_ValidCredential_ReturnsToken()
        {
            //arrange
            var command = new LoginCommand()
            {
                Email = "Jan.Kowalski@test.com",
                Password = "password123"
            };

            var employee = new Domain.Entities.User.Employee()
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "Jan.Kowalski@test.com",
                PasswordHash = "hashedPassword123"
            };

            _employeeRepositoryMock.Setup(repo => repo.GetByEmailAsync(command.Email!))
                .ReturnsAsync(employee);

            //for correct password
            _passwordHasherMock.Setup(hasher => hasher.VerifyHashedPassword(employee.PasswordHash!, command.Password!))
                .Returns(PasswordVerificationResult.Success);

            _jwtServiceMock.Setup(jwt => jwt.GenerateJwtToken(employee)).Returns("valid_jwt_token");

            //act
            var result = await _handler.Handle(command, CancellationToken.None);

            //assert
            result.Should().Be("valid_jwt_token");
        }

        [Fact()]
        public async Task Handle_InValidEmail_ThrowsNotFoundException()
        {
            //arrange
            var command = new LoginCommand()
            {
                Email = "Jan.Kowalski@test.com",
                Password = "password123"
            };

            var employee = new Domain.Entities.User.Employee()
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "Jan.Kowalski@test.com",
                PasswordHash = "hashedPassword123"
            };

            //arrange email not found
            _employeeRepositoryMock.Setup(repo => repo.GetByEmailAsync(command.Email!))
                .ReturnsAsync((Domain.Entities.User.Employee?)null);

            //act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"User not found for given email: Jan.Kowalski@test.com");
        }

        [Fact()]
        public async Task Handle_InValidPassword_ThrowsForbidException()
        {
            //arrange
            var command = new LoginCommand()
            {
                Email = "Jan.Kowalski@test.com",
                Password = "password123"
            };

            var employee = new Domain.Entities.User.Employee()
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "Jan.Kowalski@test.com",
                PasswordHash = "hashedPassword123"
            };

            //arrange email not found
            _employeeRepositoryMock.Setup(repo => repo.GetByEmailAsync(command.Email!))
                .ReturnsAsync(employee);

            _passwordHasherMock.Setup(hasher => hasher.VerifyHashedPassword(employee.PasswordHash!, command.Password!))
                .Returns(PasswordVerificationResult.Failed);

            //act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<ForbidException>()
                .WithMessage("Current password is incorrect.");
        }


    }
}