using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OvertimeManager.Application.CQRS.Employee.Compensation.Queries.GetAllActiveCompensationsByEmployeId;
using OvertimeManager.Application.CQRS.Employee.Compensation.Queries.GetAllCompensationsByEmployeId;
using OvertimeManager.Application.CQRS.Employee.Compensation.Queries.GetCompensationById;
using OvertimeManager.Application.CQRS.Employee.Compensation.Queries.GetCurrentEmployeeCompensationStatus;
using OvertimeManager.Infrastructure.Authentication;

namespace OvertimeManager.Api.Controllers
{
    [Route("api/Employee/Compensation")]
    [ApiController]
    [Authorize]
    public class EmployeeCompensationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EmployeeCompensationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CurrentEmployeeCompensationStatus([FromHeader] string authorization)
        {
            // to do
            var currentEmployeeId = TokenHelper.GetUserIdFromClaims(authorization);
            var statusDto = await _mediator.Send(new GetCurrentEmployeeCompensationStatusQuery(currentEmployeeId));
            return Ok(statusDto);
        }
        [HttpGet("requests")]
        public async Task<IActionResult> GetAllMyCompensations([FromHeader] string authorization)
        {
            // to do
            var currentEmployeeId = TokenHelper.GetUserIdFromClaims(authorization);
            var compensationDtos =  await _mediator.Send(new GetAllCompensationsByEmployeIdQuery(currentEmployeeId));

            return Ok(compensationDtos);
        }
        [HttpGet("requests/active")]
        public async Task<IActionResult> GetAllMyActiveCompensations([FromHeader] string authorization)
        {
            // to do
            var currentEmployeeId = TokenHelper.GetUserIdFromClaims(authorization);
            var compensationDtos = await _mediator.Send(new GetAllActiveCompensationsByEmployeIdQuery(currentEmployeeId));

            return Ok(compensationDtos);
        }

        [HttpGet("requests/{id}")]
        public async Task<IActionResult> GetCompensationById([FromHeader] string authorization, [FromRoute] int id)
        {
            // to do
            var currentEmployeeId = TokenHelper.GetUserIdFromClaims(authorization);
            var compensationDto = await _mediator.Send(new GetCompensationByIdQuery(currentEmployeeId, id));

            return Ok(compensationDto);
        }



        [HttpPost("requests")]
        public async Task<IActionResult> CreateCompensation([FromHeader] string authorization, 
            [FromBody] CreateCompensationDto dto)
        {
            // to do
            var currentEmployeeId = TokenHelper.GetUserIdFromClaims(authorization);
            var compensationCommand = new CreateCompensationCommand(currentEmployeeId);
            _mapper.Map(dto, compensationCommand);
            int id = await _mediator.Send(compensationCommand);
            return CreatedAtAction(nameof(GetCompensationById), new { id }, null);
        }



        [HttpPut("requests/{id}")]
        public async Task<IActionResult> UpdateCompensation([FromHeader] string authorization, 
            [FromBody] UpdateCompensationDto dto, [FromRoute] int id)
        {
            // to do
            var currentEmployeeId = TokenHelper.GetUserIdFromClaims(authorization);
            var compensationCommand = new UpdateCompensationCommand(currentEmployeeId, id);
            _mapper.Map(dto, compensationCommand);

            await _mediator.Send(compensationCommand);
            return Ok($"Request id = {id} updated");
        }

        [HttpPost("requests/{id}/cancel")]
        public async Task<IActionResult> CancelCompensation([FromHeader] string authorization, int id)
        {
            // to do
            var currentEmployeeId = TokenHelper.GetUserIdFromClaims(authorization);
            var compensationCommand = new CancelCompensationCommand(currentEmployeeId, id);

            await _mediator.Send(compensationCommand);
            return Ok($"Request id = {id} cancelled successfully.");
        }

        [HttpPost("requests/{id}/done")]
        public async Task<IActionResult> SetCompensationDone([FromHeader] string authorization, int id, [FromBody] SetCompensationDoneDto dto)
        {
            // to do
            var currentEmployeeId = TokenHelper.GetUserIdFromClaims(authorization);
            var compensationCommand = new SetCompensationDoneCommand(currentEmployeeId, id);
            _mapper.Map(dto, compensationCommand);

            await _mediator.Send(compensationCommand);
            return Ok($"Request id = {id} done");
        }

    }
}
