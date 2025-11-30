using MediatR;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.UpdatePassword
{
    public class UpdatePasswordCommand : IRequest<string>
    {
        public string? Email { get; set; }
        public string? CurrentPassword { get; set; }

        public string? NewPassword { get; set; }
        public string? ConfirmedPassword { get; set; }

    }

}
