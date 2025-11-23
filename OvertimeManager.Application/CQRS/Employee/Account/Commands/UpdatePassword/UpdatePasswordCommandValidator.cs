using FluentValidation;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.UpdatePassword
{
    public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandValidator()
        {
            RuleFor(dto => dto.Email)
                .EmailAddress()
                .WithMessage("Please provide a valid email address");

            RuleFor(dto => dto.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required");

            RuleFor(dto => dto.NewPassword)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password needs at least 6 characters");

            RuleFor(dto => dto.ConfirmedPassword)
                .NotEmpty().WithMessage("Confirmed password is required")
                .Equal(dto => dto.NewPassword).WithMessage("Passwords do not match.");
        }
    }

}
