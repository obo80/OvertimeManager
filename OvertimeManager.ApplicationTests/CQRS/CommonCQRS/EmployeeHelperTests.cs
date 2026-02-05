using FluentAssertions;
using Moq;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;
using Xunit;

namespace OvertimeManager.Application.CQRS.CommonCQRS.Tests
{
    public class EmployeeHelperTests
    {
        private Mock<IEmployeeRepository> _mockedEmployeeRepository = new ();
        public EmployeeHelperTests()
        {
            
        }
        [Fact()]
        public async Task IsEmployeeUnderManager_WithCorrectManagerId_ReturnsTrueAsync()
        {
            //arrange
            var employee = new Domain.Entities.User.Employee()
            {
                Id = 2,
                Email = "Jan.Kowalski@test.com",
                FirstName = "Jan",
                LastName = "Kowalski",
                ManagerId = 1,
            };
            var manager = new Domain.Entities.User.Employee()
            {
                Id = 1,
                Email = "Anna.Nowak@test.com",
                FirstName = "Anna",
                LastName = "Nowak",
            };

            //_mockedEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockedEmployeeRepository.Setup(repo => repo.GetByIdAsync(manager.Id))
                .ReturnsAsync(manager);
            _mockedEmployeeRepository.Setup(repo => repo.GetByIdAsync(employee.Id)).
                ReturnsAsync(employee);

            //act
            var result = await EmployeeHelper.IsEmployeeUnderManager(employee.Id, manager.Id, _mockedEmployeeRepository.Object);
            //assert
            result.Should().Be(true);
        }

        [Fact()]
        public async Task IsEmployeeUnderManager_WithIncorrectManagerId_ReturnsFalseAsync()
        {
            //arrange
            var employee = new Domain.Entities.User.Employee()
            {
                Id = 2,
                Email = "Jan.Kowalski@test.com",
                FirstName = "Jan",
                LastName = "Kowalski",
                ManagerId = 3,
            };
            var manager = new Domain.Entities.User.Employee()
            {
                Id = 1,
                Email = "Anna.Nowak@test.com",
                FirstName = "Anna",
                LastName = "Nowak",
            };

            _mockedEmployeeRepository.Setup(repo => repo.GetByIdAsync(manager.Id))
                .ReturnsAsync(manager);
            _mockedEmployeeRepository.Setup(repo => repo.GetByIdAsync(employee.Id)).
                ReturnsAsync(employee);

            //act
            var result = await EmployeeHelper.IsEmployeeUnderManager(employee.Id, manager.Id, _mockedEmployeeRepository.Object);
            //assert
            result.Should().Be(false);
        }

        [Fact()]
        public async Task IsEmployeeUnderManager_WithManagerDoesntExist_ThrowsNotFoundException()
        {
            //arrange
            var employee = new Domain.Entities.User.Employee()
            {
                Id = 2,
                Email = "Jan.Kowalski@test.com",
                FirstName = "Jan",
                LastName = "Kowalski",
                ManagerId = 3,
            };
            var manager = new Domain.Entities.User.Employee()
            {
                Id = 1,
                Email = "Anna.Nowak@test.com",
                FirstName = "Anna",
                LastName = "Nowak",
            };

            _mockedEmployeeRepository.Setup(repo => repo.GetByIdAsync(manager.Id))
                .ReturnsAsync((Domain.Entities.User.Employee)null!);
            _mockedEmployeeRepository.Setup(repo => repo.GetByIdAsync(employee.Id)).
                ReturnsAsync(employee);

            //act
            Func<Task> result = async () => await EmployeeHelper.IsEmployeeUnderManager(employee.Id, manager.Id, _mockedEmployeeRepository.Object);
            //assert
            await result.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Manager with id: 1 doesn't exist");
        }

        [Fact()]
        public async Task GetEmployeeIfUnderManager_WithManagerDoesntExist_ThrowsNotFoundExceptionAsync()
        {
            var employee = new Domain.Entities.User.Employee()
            {
                Id = 2,
                Email = "Jan.Kowalski@test.com",
                FirstName = "Jan",
                LastName = "Kowalski",
                ManagerId = 1,
            };
            var manager = new Domain.Entities.User.Employee()
            {
                Id = 1,
                Email = "Anna.Nowak@test.com",
                FirstName = "Anna",
                LastName = "Nowak",
            };

           _mockedEmployeeRepository.Setup(repo => repo.GetByIdAsync(manager.Id))
                .ReturnsAsync((Domain.Entities.User.Employee)null!);

            _mockedEmployeeRepository.Setup(repo => repo.GetByIdAsync(employee.Id)).
                ReturnsAsync(employee);

            //act
            Func<Task> result = async () => await EmployeeHelper.GetEmployeeIfUnderManager(employee.Id, manager.Id, _mockedEmployeeRepository.Object);
            //assert
            await result.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Manager with id: 1 doesn't exist");
        }

        [Fact()]
        public async Task GetEmployeeIfUnderManager_WithEmplpoyeeDoesntExist_ThrowsNotFoundExceptionAsync()
        {
            var employee = new Domain.Entities.User.Employee()
            {
                Id = 2,
                Email = "Jan.Kowalski@test.com",
                FirstName = "Jan",
                LastName = "Kowalski",
                ManagerId = 1,
            };
            var manager = new Domain.Entities.User.Employee()
            {
                Id = 1,
                Email = "Anna.Nowak@test.com",
                FirstName = "Anna",
                LastName = "Nowak",
            };


            _mockedEmployeeRepository.Setup(repo => repo.GetByIdAsync(manager.Id))
                .ReturnsAsync(manager);

            _mockedEmployeeRepository.Setup(repo => repo.GetByIdAsync(employee.Id)).
                ReturnsAsync((Domain.Entities.User.Employee)null!);

            //act
            Func<Task> result = async () => await EmployeeHelper.GetEmployeeIfUnderManager(employee.Id, manager.Id, _mockedEmployeeRepository.Object);
            //assert
            await result.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Employee with id: 2 not found under the current manager");
        }

        [Fact()]
        public async Task GetEmployeeIfUnderManager_WithCorrectData_ReturnsEmployee()
        {
            var employee = new Domain.Entities.User.Employee()
            {
                Id = 2,
                Email = "Jan.Kowalski@test.com",
                FirstName = "Jan",
                LastName = "Kowalski",
                ManagerId = 1,
            };
            var manager = new Domain.Entities.User.Employee()
            {
                Id = 1,
                Email = "Anna.Nowak@test.com",
                FirstName = "Anna",
                LastName = "Nowak",
            };
                _mockedEmployeeRepository.Setup(repo => repo.GetByIdAsync(manager.Id))
                .ReturnsAsync(manager);

            _mockedEmployeeRepository.Setup(repo => repo.GetByIdAsync(employee.Id)).
                ReturnsAsync(employee);

            //act
            var employeeResult = await EmployeeHelper.GetEmployeeIfUnderManager(employee.Id, manager.Id, _mockedEmployeeRepository.Object);
            //assert
            employeeResult.Should().NotBeNull();
            employeeResult.Id.Should().Be(2);
            employeeResult.Email.Should().Be("Jan.Kowalski@test.com");
        }

        [Fact()]
        public async Task IsManagerEmployeeRequest_WithRequestDoesntExist_ThrowsNotFoundExceptionAsync()
        {
            OvertimeRequestBase request = null!;
            var manager = new Domain.Entities.User.Employee()
            {
                Id = 1,
                Email = "Anna.Nowak@test.com",
                FirstName = "Anna",
                LastName = "Nowak",
            };

            _mockedEmployeeRepository.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(manager);

            //act
            Func<Task> result = async () => await EmployeeHelper.IsManagerEmployeeRequest(request, 1, 1, _mockedEmployeeRepository.Object);
            //assert
            await result.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Overtime request with id: 1 doesn't exist");
        }

        [Fact()]
        public async Task IsManagerEmployeeRequest_WithManagerDoesntExist_ThrowsNotFoundExceptionAsync()
        {
            OvertimeRequestBase request = new Mock<OvertimeRequestBase>().Object;
            _mockedEmployeeRepository.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync((Domain.Entities.User.Employee)null!);

            //act
            Func<Task> result = async () => await EmployeeHelper.IsManagerEmployeeRequest(request, 1, 1, _mockedEmployeeRepository.Object);
            //assert
            await result.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Manager with id: 1 doesn't exist");

        }

        [Fact()]
        public async Task IsManagerEmployeeRequest_WithCorrectManager_ReturnsTrueAsync()
        {
            var currentManagerId = 1; //input manager id

            var employee = new Domain.Entities.User.Employee()
            {
                Id = 2,
                Email = "Jan.Kowalski@test.com",
                FirstName = "Jan",
                LastName = "Kowalski",
                ManagerId = currentManagerId,
            };
            var manager = new Domain.Entities.User.Employee()
            {
                Id = 1,
                Email = "Anna.Nowak@test.com",
                FirstName = "Anna",
                LastName = "Nowak",
            };

            OvertimeRequestBase request = new OvertimeRequest()
            {
                Id = 1,
                RequestedForEmployee = employee,
            };
            _mockedEmployeeRepository.Setup(repo => repo.GetByIdAsync(currentManagerId))
                .ReturnsAsync(manager);

            //act
            var result = await EmployeeHelper.IsManagerEmployeeRequest(request, request.Id, currentManagerId, _mockedEmployeeRepository.Object);
            //assert
            result.Should().Be(true);
        }

        [Fact()]
        public async Task IsManagerEmployeeRequest_WithCorrectManager_ReturnsFalseAsync()
        {
            var currentManagerId = 3; //input manager id

            var employee = new Domain.Entities.User.Employee()
            {
                Id = 2,
                Email = "Jan.Kowalski@test.com",
                FirstName = "Jan",
                LastName = "Kowalski",
                ManagerId = currentManagerId,
            };
            var manager = new Domain.Entities.User.Employee()
            {
                Id = 1,
                Email = "Anna.Nowak@test.com",
                FirstName = "Anna",
                LastName = "Nowak",
            };

            OvertimeRequestBase request = new OvertimeRequest()
            {
                Id = 1,
                RequestedForEmployee = employee,
            };
            _mockedEmployeeRepository.Setup(repo => repo.GetByIdAsync(currentManagerId))
                .ReturnsAsync(manager);

            //act
            var result = await EmployeeHelper.IsManagerEmployeeRequest(request, request.Id, currentManagerId, _mockedEmployeeRepository.Object);
            //assert
            result.Should().Be(false);
        }

    }
}