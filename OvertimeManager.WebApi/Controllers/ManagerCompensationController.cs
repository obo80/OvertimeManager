using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Manager.Compensation.Commands.ApproveCurrentManagerEmployeesCompensationRequestById;
using OvertimeManager.Application.CQRS.Manager.Compensation.Commands.CreateCompensationByManager;
using OvertimeManager.Application.CQRS.Manager.Compensation.Commands.RejectCurrentManagerEmployeesCompensationRequestById;
using OvertimeManager.Application.CQRS.Manager.Compensation.Commands.UpdateCompensationByManager;
using OvertimeManager.Application.CQRS.Manager.Compensation.DTOs;
using OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeeByIdActiveCompensations;
using OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeeByIdCompensations;
using OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeeByIdCompensationStatus;
using OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeesActiveCompensations;
using OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeesCompensationRequestById;
using OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeesCompensations;
using OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeesOvertimeStatus;
using OvertimeManager.Infrastructure.Authentication;

namespace OvertimeManager.Api.Controllers
{
    [Route("api/Manager/Compensation")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ManagerCompensationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ManagerCompensationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet]
        [HttpGet("status")]
        public async Task<IActionResult> GetCurrentManagerEmployeesCompensationStatus([FromHeader] string authorization, 
            [FromQuery] FromQueryOptions queryOptions)
        {

            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var statusDtos = await _mediator.Send(
                new GetCurrentManagerEmployeesOvertimeStatusQuery(currentManagerId, queryOptions));
            return Ok(statusDtos);
        }

        [HttpGet("requests/{id}")]
        public async Task<IActionResult> GetCurrentManagerEmployeesCompensationRequestById([FromHeader] string authorization, 
            [FromRoute] int id)
        {

            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var compensationDtos =
                await _mediator.Send(new GetCurrentManagerEmployeesCompensationRequestByIdQuery(currentManagerId, id));

            return Ok(compensationDtos);
        }


        [HttpGet("requests")]
        public async Task<IActionResult> GetCurrentManagerEmployeesCompensations([FromHeader] string authorization,
            [FromQuery] FromQueryOptions queryOptions)
        {

            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var employeeCompensationRequestsDtos = await _mediator.Send(
                new GetCurrentManagerEmployeesCompensationsQuery(currentManagerId, queryOptions));

            return Ok(employeeCompensationRequestsDtos);
        }


        [HttpGet("requests/active")]
        public async Task<IActionResult> GetCurrentManagerEmployeesActiveCompensations([FromHeader] string authorization, 
            [FromQuery] FromQueryOptions queryOptions)
        {

            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var compensationDtos = await _mediator.Send(
                new GetCurrentManagerEmployeesActiveCompensationsQuery(currentManagerId, queryOptions));

            return Ok(compensationDtos);

        }

        [HttpGet("Employee/{id}")]
        [HttpGet("Employee/{id}/status")]
        public async Task<IActionResult> GetCurrentManagerEmployeeByIdCompensationStatus([FromHeader] string authorization, 
            [FromRoute] int id)
        {

            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var statusDto = await _mediator.Send(new GetCurrentManagerEmployeeByIdCompensationStatusQuery(currentManagerId, id));
            return Ok(statusDto);
        }

        [HttpGet("Employee/{id}/requests")]
        public async Task<IActionResult> GetCurrentManagerEmployeeByIdCompensations([FromHeader] string authorization, 
            [FromRoute] int id, [FromQuery] FromQueryOptions queryOptions)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var compensationDtos =
                await _mediator.Send(new GetCurrentManagerEmployeeByIdCompensationsQuery(currentManagerId, id, queryOptions));

            return Ok(compensationDtos);
        }

        [HttpGet("Employee/{id}/requests/active")]
        public async Task<IActionResult> GetCurrentManagerEmployeeByIdActiveCompensations([FromHeader] string authorization, 
            [FromRoute] int id, [FromQuery] FromQueryOptions queryOptions)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var compensationDtos = await _mediator.Send(
                new GetCurrentManagerEmployeeByIdActiveCompensationsQuery(currentManagerId, id, queryOptions));

            return Ok(compensationDtos);
        }

        [HttpPost("Employee/{id}")]
        public async Task<IActionResult> CreateCompensationByManager([FromHeader] string authorization, [FromRoute] int id, 
            [FromBody] CreateCompensationByManagerDto dto)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var compensationCommand = new CreateCompensationByManagerCommand(currentManagerId, id);
            _mapper.Map(dto, compensationCommand);
            int compensationId = await _mediator.Send(compensationCommand);
            return CreatedAtAction(nameof(GetCurrentManagerEmployeesCompensationRequestById), new { compensationId }, null);
        }

        [HttpPut("Employee/{id}")]
        public async Task<IActionResult> UpdateCompensationByManagerCommandByRequestId([FromHeader] string authorization,
            [FromBody] UpdateCompensationDto dto, [FromRoute] int id)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var compensationCommand = new UpdateCompensationByManagerCommand(currentManagerId, id);
            _mapper.Map(dto, compensationCommand);

            await _mediator.Send(compensationCommand);
            return Ok($"Compensation Request id = {id} updated");
        }


        [HttpPost("requests/{id}/approve")]
        public async Task<IActionResult> ApproveCurrentManagerEmployeesCompensationRequestById([FromHeader] string authorization, 
            [FromRoute] int id)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            await _mediator.Send(new ApproveCurrentManagerEmployeesCompensationRequestByIdCommand(currentManagerId, id));

            return Ok($"Compensation Request id = {id} approved");
        }

        //individual request approval
        [HttpPost("requests/{id}/reject")]
        public async Task<IActionResult> RejectCurrentManagerEmployeesCompensationRequestById([FromHeader] string authorization, 
            [FromRoute] int id)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            await _mediator.Send(new RejectCurrentManagerEmployeesCompensationRequestByIdCommand(currentManagerId, id));

            return Ok($"Compensation Request id= {id} rejected");
        }

    }
}
