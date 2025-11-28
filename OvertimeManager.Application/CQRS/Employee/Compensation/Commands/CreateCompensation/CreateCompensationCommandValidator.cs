using FluentValidation;

namespace OvertimeManager.Api.Controllers
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