using FluentValidation;

namespace OvertimeManager.Api.Controllers
{
    public class SetOvertimeDoneCommandValidator :AbstractValidator<SetOvertimeDoneDto>
    {
        public SetOvertimeDoneCommandValidator()
        {
            RuleFor(x => x.ActualTime)
                .Custom((value, context) =>
                {
                    if (value.HasValue && value <= 0)
                    {
                        context.AddFailure("ActualTime must be greater than zero.");
                    }
                });
        }
    }
}