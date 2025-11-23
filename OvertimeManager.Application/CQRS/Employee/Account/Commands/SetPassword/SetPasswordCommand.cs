using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.SetPassword
{
    public class SetPasswordCommand : IRequest<string>
    {
        public string AuthorizationToken { get; set; }
        public string? Email { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmedPassword { get; set; }
        public SetPasswordCommand(string authorization)
        {
            AuthorizationToken = authorization;
        }


    }

}
