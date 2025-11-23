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
        private string? _authorizationToken;

        public string? Email { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmedPassword { get; set; }

        public void SetAuthorizationToken(string authorizationToken)
            => _authorizationToken = authorizationToken;
        
        public string GetAuthorizationToken()
            => _authorizationToken ?? string.Empty;
        
    }

}
