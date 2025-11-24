using MediatR;
using Microsoft.AspNet.Identity;
using OvertimeManager.Application.Common;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.SetPassword
{
    public class SetPasswordCommandHandler : IRequestHandler<SetPasswordCommand, string>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public SetPasswordCommandHandler(IEmployeeRepository employeeRepository, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _employeeRepository = employeeRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }
        public async Task<string> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            var authorizationEmail = TokenHelper.GetUserEmailFromClaims(request.AuthorizationToken);
            if (authorizationEmail != request.Email)
            {
                throw new UnauthorizedException("You are not authorized to set password for this email.");
            }

            var employee = await _employeeRepository.GetByEmailAsync(request.Email!);
            if (employee is null)
            {
                throw new NotFoundException("User not found.", request.Email!);
            }
            if (!employee.MustChangePassword)
            {
                throw new UnauthorizedException("Password change is not required for this user.");
            }

            var newHashedPassword = _passwordHasher.HashPassword(request.NewPassword!);
            employee.PasswordHash = newHashedPassword;
            employee.MustChangePassword = false;

            await _employeeRepository.SaveChangesAsync();

            var newToken = _jwtService.GenerateJwtToken(employee);
            return newToken;
        }
    }

}
