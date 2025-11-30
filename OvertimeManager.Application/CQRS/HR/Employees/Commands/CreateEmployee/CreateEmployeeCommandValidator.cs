using FluentValidation;
using OvertimeManager.Application.CQRS.HR.Employees.DTOs;

namespace OvertimeManager.Application.CQRS.HR.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator :AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(dto => dto.Email)
                .EmailAddress()
                .WithMessage("Please provide a valid email address");

            RuleFor(dto => dto.FirstName)
                .NotEmpty().WithMessage("Please provide first name");

            RuleFor(dto => dto.LastName)
                .NotEmpty().WithMessage("Please provide last name");

        }
    }
}
