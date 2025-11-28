using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Application.CQRS.Manager.Overtime.Commands.ApproveCurrentManagerEmployeesOvertimeRequestById;
using OvertimeManager.Application.CQRS.Manager.Overtime.Commands.RejectCurrentManagerEmployeesOvertimeRequestById;
using OvertimeManager.Application.CQRS.Manager.Overtime.DTOs;
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
        public async Task<IActionResult> GetCurrentManagerEmployeesOvertimeStatus([FromHeader] string authorization)
        {
            //dorzucić liczbe oczekujacych requestow?
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var statusDtos = await _mediator.Send(new GetCurrentManagerEmployeesOvertimeStatusQuery(currentManagerId));
            return Ok(statusDtos);
        }

        //individual request
        [HttpGet("requests/{id}")]
        public async Task<IActionResult> GetCurrentManagerEmployeesOvertimeRequestById([FromHeader] string authorization, [FromRoute] int id)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var overtimeDtos =
                await _mediator.Send(new GetCurrentManagerEmployeesOvertimeRequestByIdQuery(currentManagerId, id));

            return Ok(overtimeDtos);
        }

        //individual request approval
        [HttpPost("requests/{id}/approve")]
        public async Task<IActionResult> UpdateApproveCurrentManagerEmployeesOvertimeRequestById([FromHeader] string authorization, [FromRoute] int id)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            await _mediator.Send(new ApproveCurrentManagerEmployeesOvertimeRequestByIdCommand(currentManagerId, id));

            return Ok($"Overtime Request id = {id} approved");
        }

        //individual request approval
        [HttpPost("requests/{id}/reject")]
        public async Task<IActionResult> RejectCurrentManagerEmployeesOvertimeRequestById([FromHeader] string authorization, [FromRoute] int id)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            await _mediator.Send(new RejectCurrentManagerEmployeesOvertimeRequestByIdCommand(currentManagerId, id));

            return Ok($"Overtime Request id= {id} rejected");
        }


        [HttpGet("requests")]
        [HttpGet("requests/all")]
        public async Task<IActionResult> GetCurrentManagerEmployeesOvertimes([FromHeader] string authorization)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            IEnumerable<EmployeeOvertimeRequestsDto> employeeOvertimeRequestsDtos =
                await _mediator.Send(new GetCurrentManagerEmployeesOvertimesQuery(currentManagerId));

            return Ok(employeeOvertimeRequestsDtos);
        }


        [HttpGet("requests/active")]
        public async Task<IActionResult> GetCurrentManagerEmployeesActiveOvertimes([FromHeader] string authorization)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            IEnumerable<EmployeeOvertimeRequestsDto> overtimeDtos =
                await _mediator.Send(new GetCurrentManagerEmployeesActiveOvertimesQuery(currentManagerId));

            return Ok(overtimeDtos);
        }

        [HttpGet("Employee/{id}")]
        [HttpGet("Employee/{id}/status")]
        public async Task<IActionResult> GetCurrentManagerEmployeeByIdOvertimeStatus([FromHeader] string authorization, [FromRoute] int id)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var statusDto = await _mediator.Send(new GetCurrentManagerEmployeeByIdOvertimeStatusQuery(currentManagerId, id));
            return Ok(statusDto);
        }

        [HttpGet("Employee/{id}/requests")]
        public async Task<IActionResult> GetCurrentManagerEmployeeByIdOvertimes([FromHeader] string authorization, [FromRoute] int id)
        {
            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            IEnumerable<GetOvertimeDto> overtimeDtos =
                await _mediator.Send(new GetCurrentManagerEmployeeByIdOvertimesQuery(currentManagerId, id));

            return Ok(overtimeDtos);
        }

        [HttpGet("Employee/{id}/requests/active")]
        public async Task<IActionResult> GetCurrentManagerEmployeeByIdActiveOvertimes([FromHeader] string authorization, [FromRoute] int id)
        {

            var currentManagerId = TokenHelper.GetUserIdFromClaims(authorization);
            var employeeId = id;
            IEnumerable<GetOvertimeDto> overtimeDtos =
                await _mediator.Send(new GetCurrentManagerEmployeeByIdActiveOvertimesQuery(currentManagerId, id));

            return Ok(overtimeDtos);
        }


    }
}
