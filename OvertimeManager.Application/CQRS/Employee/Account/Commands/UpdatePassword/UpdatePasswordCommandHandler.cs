using MediatR;
using Microsoft.AspNet.Identity;
using OvertimeManager.Application.Common;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, string>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public UpdatePasswordCommandHandler(IEmployeeRepository employeeRepository, IPasswordHasher passwordHasher, 
            IJwtService jwtService)
        {
            _employeeRepository = employeeRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }
        public async Task<string> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            
            var employee = await _employeeRepository.GetByEmailAsync(request.Email!);
            if (employee is null)
                throw new NotFoundException($"Employee not found for given email: {request.Email}");

            if (employee.MustChangePassword)
                throw new ForbidException("You must set a new password before updating it.");
            
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(employee.PasswordHash, request.CurrentPassword);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                throw new ForbidException("Current password is incorrect.");
            
            var newHashedPassword = _passwordHasher.HashPassword(request.NewPassword);
            employee.PasswordHash = newHashedPassword;

            await _employeeRepository.SaveChangesAsync();

            var newToken = _jwtService.GenerateJwtToken(employee);
            return newToken;
        }
    }

}
