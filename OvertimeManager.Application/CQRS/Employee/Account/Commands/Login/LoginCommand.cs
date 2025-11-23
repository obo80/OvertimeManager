using MediatR;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.Login
{
    public class LoginCommand : IRequest<string>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
