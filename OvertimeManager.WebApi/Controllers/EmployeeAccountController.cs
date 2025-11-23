using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using OvertimeManager.Application.CQRS.Employee.Account.Commands.Login;
using OvertimeManager.Application.CQRS.Employee.Account.Commands.SetPassword;
using OvertimeManager.Application.CQRS.Employee.Account.Commands.UpdatePassword;

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
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            string token = await _mediator.Send(command);
            return Ok(token);
        }

        [HttpPost("set-password")]
        public async Task<IActionResult> SetPassword([FromBody] SetPasswordCommand command, [FromHeader] string authorization)
        {
            command.SetAuthorizationToken(authorization);
            var token = await _mediator.Send(command);
            return Ok(token);
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommand command, [FromHeader] string authorization)
        {
            command.SetAuthorizationToken(authorization);
            var token = await _mediator.Send(command);
            return Ok(token);
        }
    }
}
