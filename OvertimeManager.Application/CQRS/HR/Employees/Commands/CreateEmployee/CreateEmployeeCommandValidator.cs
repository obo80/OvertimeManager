using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace OvertimeManager.Application.CQRS.HR.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator :AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(dto => dto.Email)
                .EmailAddress()
                .WithMessage("Please provide a valid email address");

            RuleFor(dto => dto.FirstName)
                .NotEmpty().WithMessage("Please provide first name");

            RuleFor(dto => dto.LastName)
                .NotEmpty().WithMessage("Please provide last name");

        }
    }
}
