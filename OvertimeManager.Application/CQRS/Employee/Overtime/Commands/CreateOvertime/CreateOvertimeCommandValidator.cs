using FluentValidation;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Commands.CreateOvertime
{
    public class CreateOvertimeCommandValidator : AbstractValidator<CreateOvertimeDto>
    {
        public CreateOvertimeCommandValidator()
        {
            RuleFor(dto => dto.RequestedTime)
                .GreaterThan(0).WithMessage("Requested time must be greater than zero");

            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(dto => dto.BusinessJustificationReason)
                .NotEmpty().WithMessage("Business justification reason is required");
        }
    }
}
