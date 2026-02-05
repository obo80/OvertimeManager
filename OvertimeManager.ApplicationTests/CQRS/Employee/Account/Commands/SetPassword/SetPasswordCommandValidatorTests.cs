using FluentValidation.TestHelper;
using OvertimeManager.Application.CQRS.Employee.Account.Commands.SetPassword;
using OvertimeManager.Application.CQRS.Employee.Account.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.SetPassword.Tests
{
    public class SetPasswordCommandValidatorTests
    {
        private readonly SetPasswordCommandValidator _validator = new();
        [Fact()]
        public void Validate_WithValidDto_ShouldNotHaveValidationError()
        {
            //arrange
            var dto = new SetPasswordDto()
            {
                Email = "Jan.Kowalski@test.com",
                NewPassword = "password123",
                ConfirmedPassword = "password123",
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
            var dto = new SetPasswordDto()
            {
                Email = "",
                NewPassword = "",
                ConfirmedPassword = "",
            };
            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
            result.ShouldHaveValidationErrorFor(x => x.NewPassword);
            result.ShouldHaveValidationErrorFor(x => x.ConfirmedPassword);
        }

        [Fact()]
        public void Validate_WithInvalidDtoEmail_ShouldHaveValidationErrors()
        {
            //arrange
            var dto = new SetPasswordDto()
            {
                Email = "Jan.Kowalski_test.com",    //invalid email format
                NewPassword = "password123",
                ConfirmedPassword = "password123",
            };
            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.Email);

        }
        [Fact()]
        public void Validate_WithInvalidDtoPasswordTooShort_ShouldHaveValidationErrors()
        {
            //arrange
            var dto = new SetPasswordDto()
            {
                Email = "Jan.Kowalski@test.com",
                NewPassword = "pass",
                ConfirmedPassword = "pass"  
            };
            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.NewPassword);
        }
        [Fact()]
        public void Validate_WithInvalidDtoPasswordsDoesNotMatch_ShouldHaveValidationErrors()
        {
            //arrange
            var dto = new SetPasswordDto()
            {
                Email = "Jan.Kowalski@test.com",    
                NewPassword = "password123",
                ConfirmedPassword = "password1234"  //does not match
            };
            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.ConfirmedPassword);
        }
    }
}