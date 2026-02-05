using FluentValidation;
using OvertimeManager.Application.CQRS.Employee.Account.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.SetPassword
{
    public class SetPasswordCommandValidator :AbstractValidator<SetPasswordDto>
    {
        public SetPasswordCommandValidator()
        {
            RuleFor(dto => dto.Email)
                .EmailAddress()
                .WithMessage("Please provide a valid email address");

            RuleFor(dto => dto.NewPassword)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password needs at least 6 characters");

            RuleFor(dto => dto.ConfirmedPassword)
                .NotEmpty().WithMessage("Confirmed password is required")
                .Equal(dto => dto.NewPassword).WithMessage("Passwords do not match.");

        }
    }

}
