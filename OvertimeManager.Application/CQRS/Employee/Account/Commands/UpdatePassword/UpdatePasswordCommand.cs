using MediatR;
using OvertimeManager.Application.CQRS.Employee.Account.Commands.SetPassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.UpdatePassword
{
    public class UpdatePasswordCommand : IRequest<string>
    {
        public string? Email { get; set; }
        public string? CurrentPassword { get; set; }

        public string? NewPassword { get; set; }
        public string? ConfirmedPassword { get; set; }
        public string AuthorizationToken { get; }
        public UpdatePasswordCommand(string authorization)
        {
            AuthorizationToken = authorization;
        }
    }

}
