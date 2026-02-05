using FluentValidation.TestHelper;
using OvertimeManager.Application.CQRS.Employee.Account.Commands.SetPassword;
using OvertimeManager.Application.CQRS.Employee.Account.Commands.UpdatePassword;
using OvertimeManager.Application.CQRS.Employee.Account.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.UpdatePassword.Tests
{
    public class UpdatePasswordCommandValidatorTests
    {
        private readonly UpdatePasswordCommandValidator _validator = new();
        [Fact()]
        public void Validate_WithValidDto_ShouldNotHaveValidationError()
        {
            //arrange
            var dto = new UpdatePasswordDto()
            {
                Email = "Jan.Kowalski@test.com",
                CurrentPassword = "oldpassword123",
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
            var dto = new UpdatePasswordDto()
            {
                Email = "",
                CurrentPassword = "",
                NewPassword = "",
                ConfirmedPassword = "",
            };
            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
            result.ShouldHaveValidationErrorFor(x => x. CurrentPassword);
            result.ShouldHaveValidationErrorFor(x => x.NewPassword);
            result.ShouldHaveValidationErrorFor(x => x.ConfirmedPassword);
        }

        [Fact()]
        public void Validate_WithInvalidDtoEmail_ShouldHaveValidationErrors()
        {
            //arrange
            var dto = new UpdatePasswordDto()
            {
                Email = "Jan.Kowalski_test.com",    //invalid email format
                CurrentPassword = "oldpassword123",
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
            var dto = new UpdatePasswordDto()
            {
                Email = "Jan.Kowalski@test.com",
                CurrentPassword = "oldpassword123",
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
            var dto = new UpdatePasswordDto()
            {
                Email = "Jan.Kowalski@test.com",
                CurrentPassword = "oldpassword123",
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