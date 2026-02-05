using FluentValidation;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Commands.CreateCompensation
{
    public class CreateCompensationCommandValidator :AbstractValidator<CreateCompensationDto>
    {
        public CreateCompensationCommandValidator()
        {
            RuleFor(dto => dto.RequestedTime)
                .GreaterThan(0).WithMessage("Requested time must be greater than zero");
        }
    }
}