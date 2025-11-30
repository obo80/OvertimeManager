using MediatR;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.SetPassword
{
    public class SetPasswordCommand : IRequest<string>
    {
        public string? Email { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmedPassword { get; set; }



    }

}
