using FluentValidation;
using OvertimeManager.Application.CQRS.HR.Employees.DTOs;

namespace OvertimeManager.Application.CQRS.HR.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator: AbstractValidator<UpdateEmployeeDto>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(dto => dto.Email)
                .EmailAddress()
                .WithMessage("Please provide a valid email address");
        }

    }
}
