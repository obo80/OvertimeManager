using FluentValidation;
using OvertimeManager.Application.CQRS.Manager.Compensation.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Commands.CreateCompensationByManager
{
    public class CreateCompensationByManagerCommandValidator : AbstractValidator<CreateCompensationByManagerDto>
    {
        public CreateCompensationByManagerCommandValidator()
        {
            RuleFor(dto => dto.RequestedTime)
                .GreaterThan(0).WithMessage("Requested time must be greater than zero");
            RuleFor(dto => dto.RequestedForEmployeeId)
                .GreaterThan(0).WithMessage("Required Employee Id must be greater than zero");
        }

    }

}
