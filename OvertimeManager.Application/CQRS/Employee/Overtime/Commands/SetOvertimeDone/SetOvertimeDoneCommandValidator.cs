using FluentValidation;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

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
                        context.AddFailure("Actual time must be greater than zero.");
                    }
                });
        }
    }
}