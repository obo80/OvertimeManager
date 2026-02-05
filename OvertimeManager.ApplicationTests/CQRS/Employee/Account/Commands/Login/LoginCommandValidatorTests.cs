using Xunit;
using OvertimeManager.Application.CQRS.Employee.Account.Commands.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using OvertimeManager.Application.CQRS.Employee.Account.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.Login.Tests
{
    public class LoginCommandValidatorTests
    {
        private readonly LoginCommandValidator _validator = new();

        [Fact()]
        public void Validate_WithValidDto_ShouldNotHaveValidationError()
        {
            //arrange
            var dto = new LoginDto()
            {
                Email = "Jan.Kowalski@test.com",
                Password = "password123"
            };
            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validate_WithInvalidDtoEmptyData_ShouldHaveValidationErrors()
        {
            //arrange
            var dto = new LoginDto()
            {
                Email = "",
                Password = ""
            };
            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact()]
        public void Validate_WithInvalidDtoEmail_ShouldHaveValidationErrors()
        {
            //arrange
            var dto = new LoginDto()
            {
                Email = "Jan.Kowalski_test.com",    //invalid email format
                //Password = "password123"
            };
            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
            //result.ShouldNotHaveValidationErrorFor(x => x.Password);
        }
    }
}