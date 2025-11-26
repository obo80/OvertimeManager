using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OvertimeManager.Application.Common;
using OvertimeManager.Application.CQRS.Employee.Overtime.Commands.CancelOvertime;
using OvertimeManager.Application.CQRS.Employee.Overtime.Commands.CreateOvertime;
using OvertimeManager.Application.CQRS.Employee.Overtime.Commands.UpdateOvertime;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Application.CQRS.Employee.Overtime.Queries.GetAllOvertimesByEmployeId;
using OvertimeManager.Application.CQRS.Employee.Overtime.Queries.GetCurrentEmployeeOvertimeStatus;
using OvertimeManager.Application.CQRS.Employee.Overtime.Queries.GetOvertimeById;

namespace OvertimeManager.Api.Controllers
{
    [Route("api/Employee/Overtime")]
    [ApiController]
    [Authorize]
    public class EmployeeOvertimeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EmployeeOvertimeController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CurrentEmployeeOvertimeStatus([FromHeader] string authorization)
        {
            var currentEmployeeId = TokenHelper.GetUserIdFromClaims(authorization);
            var statusDto = await _mediator.Send(new GetCurrentEmployeeOvertimeStatusQuery(currentEmployeeId));
            return Ok(statusDto);
        }
        [HttpGet("requests")]
        public async Task<IActionResult> GetAllMyOvertimes([FromHeader] string authorization)
        {
            var currentEmployeeId = TokenHelper.GetUserIdFromClaims(authorization);
            IEnumerable<GetOvertimeDto> overtimeDtos = 
                await _mediator.Send(new GetAllOvertimesByEmployeIdQuery(currentEmployeeId));

            return Ok(overtimeDtos);
        }
        [HttpGet("active-requests")]
        public async Task<IActionResult> GetAllMyActiveOvertimes([FromHeader] string authorization)
        {
            var currentEmployeeId = TokenHelper.GetUserIdFromClaims(authorization);
            IEnumerable<GetOvertimeDto> overtimeDtos =
                await _mediator.Send(new GetAllActiveOvertimesByEmployeIdQuery(currentEmployeeId));

            return Ok(overtimeDtos);
        }

        [HttpGet("requests/{id}")]
        public async Task<IActionResult> GetOvertimeById([FromHeader] string authorization, [FromRoute] int id)
        {
            var currentEmployeeId = TokenHelper.GetUserIdFromClaims(authorization);
            GetOvertimeDto overtimeDto = await _mediator.Send(new GetOvertimeByIdQuery(currentEmployeeId, id));

            return Ok(overtimeDto);
        }



        [HttpPost("requests")]
        public async Task<IActionResult> CreateOvertime([FromHeader] string authorization, 
            [FromBody] CreateOvertimeDto dto)
        {
            var currentEmployeeId = TokenHelper.GetUserIdFromClaims(authorization);
            var overtimeCommand = new CreateOvertimeCommand(authorization);
            _mapper.Map(dto, overtimeCommand);
            int id = await _mediator.Send(overtimeCommand);
            return CreatedAtAction(nameof(GetOvertimeById), new { id }, null);
        }



        [HttpPut("requests/{id}")]
        public async Task<IActionResult> UpdateOvertime([FromHeader] string authorization, 
            [FromBody] UpdateOvertimeDto dto, int id)
        {
            var currentEmployeeId = TokenHelper.GetUserIdFromClaims(authorization);
            var overtimeCommand = new UpdateOvertimeCommand(currentEmployeeId, id);
            _mapper.Map(dto, overtimeCommand);

            await _mediator.Send(overtimeCommand);
            return Ok($"Request id = {id} updated");
        }

        [HttpPost("requests/{id}/cancel")]
        public async Task<IActionResult> CancelOvertime([FromHeader] string authorization, int id)
        {
            var currentEmployeeId = TokenHelper.GetUserIdFromClaims(authorization);
            var overtimeCommand = new CancelOvertimeCommand(currentEmployeeId, id);

            await _mediator.Send(overtimeCommand);
            return Ok($"Request id = {id} cancelled successfully.");
        }

        [HttpPost("requests/{id}/done")]
        public async Task<IActionResult> SetOvertimeDone([FromHeader] string authorization, int id, [FromBody] SetOvertimeDoneDto dto)
        {
            var currentEmployeeId = TokenHelper.GetUserIdFromClaims(authorization);
            var overtimeCommand = new SetOvertimeDoneCommand(currentEmployeeId, id);
            _mapper.Map(dto, overtimeCommand);

            await _mediator.Send(overtimeCommand);
            return Ok($"Request id = {id} done");
        }

    }
}
