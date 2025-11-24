using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using OvertimeManager.Application.CQRS.Employee.Account.Commands.Login;
using OvertimeManager.Application.CQRS.Employee.Account.Commands.SetPassword;
using OvertimeManager.Application.CQRS.Employee.Account.Commands.UpdatePassword;
using OvertimeManager.Application.CQRS.Employee.Account.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OvertimeManager.Api.Controllers
{
    [Route("api/Employee/Account")]
    [ApiController]
    public class EmployeeAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // POST api/Employee/Account/login
        [HttpPost("login")]
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
        public async Task<IActionResult> SetPassword([FromBody] SetPassowordDto dto, [FromHeader] string authorization)
        {
            var command = new SetPasswordCommand(authorization)
            {
                Email = dto.Email,
                NewPassword = dto.NewPassword,
                ConfirmedPassword = dto.ConfirmedPassword
            };
            var newToken = await _mediator.Send(command);
            return Ok(newToken);
        }

        // POST api/Employee/Account/update-password
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto dto, [FromHeader] string authorization)
        {
            var command = new UpdatePasswordCommand(authorization)
            {
                Email = dto.Email,
                CurrentPassword = dto.CurrentPassword,
                NewPassword = dto.NewPassword,
                ConfirmedPassword = dto.ConfirmedPassword
            };
            var newToken = await _mediator.Send(command);
            return Ok(newToken);
        }


    }
}
