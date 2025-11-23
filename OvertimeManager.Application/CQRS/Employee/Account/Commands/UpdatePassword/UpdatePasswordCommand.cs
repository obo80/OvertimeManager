using MediatR;
using OvertimeManager.Application.CQRS.Employee.Account.Commands.SetPassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.UpdatePassword
{
    public  class UpdatePasswordCommand : SetPasswordCommand, IRequest<string>
    {
        public string CurrentPassword { get; set; }
    }

}
