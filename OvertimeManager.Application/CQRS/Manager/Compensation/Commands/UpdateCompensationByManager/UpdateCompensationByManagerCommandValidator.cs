using FluentValidation;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Commands.UpdateCompensationByManager
{
    public class UpdateCompensationByManagerCommandValidator :AbstractValidator<UpdateCompensationByManagerCommand>
    {
        public UpdateCompensationByManagerCommandValidator()
        {
            RuleFor(dto => dto.RequestedTime)
    .           GreaterThan(0).WithMessage("Requested time must be greater than zero");
            RuleFor(dto => dto.RequestedForEmployeeId)
                .GreaterThan(0).WithMessage("Required Employee Id must be greater than zero");
        }
    }
}
