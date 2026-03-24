using FluentValidation.TestHelper;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using Xunit;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Commands.CreateOvertime.Tests
{
    public class CreateOvertimeCommandValidatorTests
    {
        private readonly CreateOvertimeCommandValidator _validator = new();

        [Fact()]
        public void Validate_WithValidDto_ShouldNotHaveValidationError()
        {
            //arrange
            var dto = new CreateOvertimeDto
            {
                Name = "Overtime Work",
                RequestedTime = 2,
                BusinessJustificationReason = "Project deadline"
            };

            //act
            var result = _validator.TestValidate(dto);

            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory()]
        [InlineData(0.0)]
        [InlineData(-1.0)]
        public void Validate_WithInvalidRequestedTime_ShouldHaveValidationError(double requestedTime)
        {
            //arrange
            var dto = new CreateOvertimeDto
            {
                RequestedTime = requestedTime,
            };
            //act
            var result = _validator.TestValidate(dto);
            //assert
            result.ShouldHaveValidationErrorFor(x => x.RequestedTime);
        }

        [Fact()]
        public void Validate_WithEmptyName_ShouldHaveValidationError()
        {
            //arrange
            var dto = new CreateOvertimeDto
            {
                Name = string.Empty
            };
            //act
            var result = _validator.TestValidate(dto);
            //assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact()]
        public void Validate_WithEmptyBusinessJustificationReason_ShouldHaveValidationError()
        {
            //arrange
            var dto = new CreateOvertimeDto
            {
                BusinessJustificationReason = string.Empty
            };
            //act
            var result = _validator.TestValidate(dto);
            //assert
            result.ShouldHaveValidationErrorFor(x => x.BusinessJustificationReason);
        }
    }
}