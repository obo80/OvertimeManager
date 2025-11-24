using FluentValidation;
using OvertimeManager.Application.CQRS.Employee.Account.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginDto>
    {
        public LoginCommandValidator()
        {
            RuleFor(dto => dto.Email)
                .EmailAddress()
                .WithMessage("Please provide a valid email address");
            RuleFor(dto => dto.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
