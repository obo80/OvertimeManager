using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.HR.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator: AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(dto => dto.Email)
                .EmailAddress()
                .WithMessage("Please provide a valid email address");
        }

    }
}
