using MediatR;
using Microsoft.AspNet.Identity;
using OvertimeManager.Application.Common;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.UpdatePassword
{
    class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, string>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public UpdatePasswordCommandHandler(IEmployeeRepository employeeRepository, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _employeeRepository = employeeRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }
        public async Task<string> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var authorizationEmail = TokenHelper.GetUserEmailFromClaims(request.GetAuthorizationToken());
            if (authorizationEmail != request.Email)
                throw new Domain.Exceptions.UnauthorizedAccessException("You are not authorized to set password for this email.");
            
            var employee = await _employeeRepository.GetByEmail(request.Email);
            if (employee is null)
                throw new NotFoundException("User not found.", request.Email);
            
            if (employee.MustChangePassword)
                throw new Domain.Exceptions.UnauthorizedAccessException("You must set a new password before updating it.");
            
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(employee.PasswordHash, request.CurrentPassword);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                throw new Domain.Exceptions.UnauthorizedAccessException("Current password is incorrect.");
            
            var newHashedPassword = _passwordHasher.HashPassword(request.NewPassword);
            employee.PasswordHash = newHashedPassword;

            await _employeeRepository.SaveChanges();

            var newToken = _jwtService.GenerateJwtToken(employee);
            return newToken;
        }
    }

}
