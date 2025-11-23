using FluentValidation;
using MediatR;
using Microsoft.AspNet.Identity;
using OvertimeManager.Application.Common;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Account.Commands.Login
{
    public class LoginCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(IEmployeeRepository employeeRepository, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _employeeRepository = employeeRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByEmail(request.Email);
            if (employee is null)
                throw new NotFoundException("User not found.", request.Email);


            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(employee.PasswordHash, request.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                throw new Domain.Exceptions.UnauthorizedAccessException("Current password is incorrect.");

            var token = _jwtService.GenerateJwtToken(employee);
            return token;
        }
    }

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(dto => dto.Email)
                .EmailAddress()
                .WithMessage("Please provide a valid email address");
            RuleFor(dto => dto.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
