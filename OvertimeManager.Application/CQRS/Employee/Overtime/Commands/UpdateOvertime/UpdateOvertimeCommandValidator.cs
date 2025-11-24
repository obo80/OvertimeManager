using FluentValidation;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Commands.UpdateOvertime
{
    class UpdateOvertimeCommandValidator : AbstractValidator<UpdateOvertimeDto>
    {
        public UpdateOvertimeCommandValidator()
        {
            RuleFor(x => x.RequestedTime)
                .Custom((value, context) =>
                {
                    if (value.HasValue && value <= 0)
                    {
                        context.AddFailure("Requested time must be greater than zero.");
                    }
                });
        }
    }



}
