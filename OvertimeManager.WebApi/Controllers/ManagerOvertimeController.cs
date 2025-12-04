using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Manager.Overtime.Commands.ApproveCurrentManagerEmployeesOvertimeRequestById;
using OvertimeManager.Application.CQRS.Manager.Overtime.Commands.RejectCurrentManagerEmployeesOvertimeRequestById;
using OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeeByIdActiveOvertimes;
using OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeeByIdOvertimes;
using OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeeByIdOvertimeStatus;
using OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeesActiveOvertimes;
using OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeesOvertimeRequestById;
using OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeesOvertimes;
using OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeesOvertimeStatus;
using OvertimeManager.Infrastructure.Authentication;

namespace OvertimeManager.Api.Controllers
{
    [Route("api/Manager/Overtime")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ManagerOvertimeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ManagerOvertimeController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpGet("status")]
        public async Task<IActionResult> GetCurrentManagerEmployeesOvertimeStatus([FromHeader] string authorization,
            [FromQuery] FromQueryOptions queryOptions)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var statusDtos = await _mediator.Send(new GetCurrentManagerEmployeesOvertimeStatusQuery(currentManagerId, queryOptions));
            return Ok(statusDtos);
        }

        [HttpGet("requests/{id}")]
        public async Task<IActionResult> GetCurrentManagerEmployeesOvertimeRequestById([FromHeader] string authorization, [FromRoute] int id)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var overtimeDtos =
                await _mediator.Send(new GetCurrentManagerEmployeesOvertimeRequestByIdQuery(currentManagerId, id));

            return Ok(overtimeDtos);
        }

        [HttpPost("requests/{id}/approve")]
        public async Task<IActionResult> UpdateApproveCurrentManagerEmployeesOvertimeRequestById([FromHeader] string authorization, [FromRoute] int id)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            await _mediator.Send(new ApproveCurrentManagerEmployeesOvertimeRequestByIdCommand(currentManagerId, id));

            return Ok($"Overtime Request id = {id} approved");
        }


        [HttpPost("requests/{id}/reject")]
        public async Task<IActionResult> RejectCurrentManagerEmployeesOvertimeRequestById([FromHeader] string authorization, 
            [FromRoute] int id)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            await _mediator.Send(new RejectCurrentManagerEmployeesOvertimeRequestByIdCommand(currentManagerId, id));

            return Ok($"Overtime Request id= {id} rejected");
        }


        [HttpGet("requests")]
        [HttpGet("requests/all")]
        public async Task<IActionResult> GetCurrentManagerEmployeesOvertimes([FromHeader] string authorization, 
            [FromQuery] FromQueryOptions queryOptions)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var overtimeDtos = await _mediator.Send(new GetCurrentManagerEmployeesOvertimesQuery(currentManagerId, queryOptions));

            return Ok(overtimeDtos);
        }


        [HttpGet("requests/active")]
        public async Task<IActionResult> GetCurrentManagerEmployeesActiveOvertimes([FromHeader] string authorization, 
            [FromQuery] FromQueryOptions queryOptions)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var overtimeDtos =
                await _mediator.Send(new GetCurrentManagerEmployeesActiveOvertimesQuery(currentManagerId, queryOptions));

            return Ok(overtimeDtos);
        }

        [HttpGet("Employee/{id}")]
        [HttpGet("Employee/{id}/status")]
        public async Task<IActionResult> GetCurrentManagerEmployeeByIdOvertimeStatus([FromHeader] string authorization, 
            [FromRoute] int id)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var statusDto = await _mediator.Send(new GetCurrentManagerEmployeeByIdOvertimeStatusQuery(currentManagerId, id));
            return Ok(statusDto);
        }

        [HttpGet("Employee/{id}/requests")]
        public async Task<IActionResult> GetCurrentManagerEmployeeByIdOvertimes([FromHeader] string authorization, 
            [FromRoute] int id, [FromQuery] FromQueryOptions queryOptions)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var overtimeDtos = await _mediator.Send(new GetCurrentManagerEmployeeByIdOvertimesQuery(currentManagerId, id, queryOptions));

            return Ok(overtimeDtos);
        }

        [HttpGet("Employee/{id}/requests/active")]
        public async Task<IActionResult> GetCurrentManagerEmployeeByIdActiveOvertimes([FromHeader] string authorization, 
            [FromRoute] int id, [FromQuery] FromQueryOptions queryOptions)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var overtimeDtos = await _mediator.Send(new GetCurrentManagerEmployeeByIdActiveOvertimesQuery(currentManagerId, id, queryOptions));

            return Ok(overtimeDtos);
        }


    }
}
