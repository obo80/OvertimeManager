using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OvertimeManager.Application.CQRS.Employee.Account.Commands.Login;
using OvertimeManager.Application.CQRS.Employee.Account.Commands.SetPassword;
using OvertimeManager.Application.CQRS.Employee.Account.Commands.UpdatePassword;
using OvertimeManager.Application.CQRS.Employee.Account.DTOs;
using OvertimeManager.Infrastructure.Authentication;


namespace OvertimeManager.Api.Controllers
{
    [Route("api/Employee/Account")]
    [ApiController]
    [Authorize]
    public class EmployeeAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // POST api/Employee/Account/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var command = new LoginCommand
            {
                Email = dto.Email,
                Password = dto.Password
            };

            string token = await _mediator.Send(command);
            return Ok(token);
        }
        // POST api/Employee/Account/set-password
        [HttpPost("set-password")]
        public async Task<IActionResult> SetPassword([FromBody] SetPasswordDto dto, [FromHeader] string authorization)
        {
            var command = new SetPasswordCommand()
            {
                Email = dto.Email,
                NewPassword = dto.NewPassword,
                ConfirmedPassword = dto.ConfirmedPassword
            };

            if (TokenHelper.IsEmployeeAuthorizedByEmail(authorization, command.Email!))
            {
                var newToken = await _mediator.Send(command);
                return Ok(newToken);
            }
            return Unauthorized("You are not authorized to set password for this email.");
        }

        // POST api/Employee/Account/update-password
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto dto, [FromHeader] string authorization)
        {
            var command = new UpdatePasswordCommand()
            {
                Email = dto.Email,
                CurrentPassword = dto.CurrentPassword,
                NewPassword = dto.NewPassword,
                ConfirmedPassword = dto.ConfirmedPassword
            };
            if (TokenHelper.IsEmployeeAuthorizedByEmail(authorization, command.Email!))
            {
                var newToken = await _mediator.Send(command);
                return Ok(newToken);
            }
            return Unauthorized("You are not authorized to update password for this email.");
        }


    }
}
