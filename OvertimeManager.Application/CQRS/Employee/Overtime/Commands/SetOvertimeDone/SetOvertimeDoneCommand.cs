using FluentValidation;
using MediatR;

namespace OvertimeManager.Api.Controllers
{
    public class SetOvertimeDoneCommand : IRequest
    {
        public double? ActualTime { get; set; }

        public int CurrentEmployeeId { get; }
        public int OvertimeId { get; }

        public SetOvertimeDoneCommand(int currentEmployeeId, int id)
        {
            CurrentEmployeeId = currentEmployeeId;
            OvertimeId = id;
        }

    }

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