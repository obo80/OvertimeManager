using FluentValidation;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Commands.UpdateCompensation
{
    public class UpdateCompensationCommandValidator : AbstractValidator<UpdateCompensationDto>
    {
        public UpdateCompensationCommandValidator()
        {
            RuleFor(dto => dto.RequestedTime)
                .GreaterThan(0).WithMessage("Requested time must be greater than zero");
        }
    }    
}