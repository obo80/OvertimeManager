using FluentValidation;

namespace OvertimeManager.Api.Controllers
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