using Xunit;
using OvertimeManager.Application.CQRS.Employee.Compensation.Commands.CreateCompensation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Commands.CreateCompensation.Tests
{
    public class CreateCompensationCommandValidatorTests
    {
        private readonly CreateCompensationCommandValidator _validator = new();

        [Fact()]
        public void Validate_WithValidDto_ShouldNotHaveValidationError()
        {
            //arrange
            var dto = new CreateCompensationDto
            {
                RequestedTime = 2.0
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
            var dto = new CreateCompensationDto
            {
                RequestedTime = requestedTime
            };
            //act
            var result = _validator.TestValidate(dto);
            //assert
            result.ShouldHaveValidationErrorFor(x => x.RequestedTime);
        }
    }
}