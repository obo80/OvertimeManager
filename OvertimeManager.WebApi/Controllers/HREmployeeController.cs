using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OvertimeManager.Application.CQRS.HR.Employees.Commands.CreateEmployee;
using OvertimeManager.Application.CQRS.HR.Employees.Commands.DeleteEmployee;
using OvertimeManager.Application.CQRS.HR.Employees.Commands.UpdateEmployee;
using OvertimeManager.Application.CQRS.HR.Employees.DTOs;
using OvertimeManager.Application.CQRS.HR.Employees.Queries.GetAllEmployees;
using OvertimeManager.Application.CQRS.HR.Employees.Queries.GetEmployeeById;
using OvertimeManager.Application.CQRS.HR.Employees.Queries.GetTokenEmployeeById;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OvertimeManager.Api.Controllers
{
    [Route("api/HR/Employees")]
    [ApiController]
    public class HREmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public HREmployeeController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        // GET: api/HR/Employees
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employeesDto = await _mediator.Send(new GetAllEmployeesQuery());
            return Ok(employeesDto);
        }

        // GET api/HR/Employees/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
        {
            var employeeDto = await _mediator.Send(new GetEmployeeByIdQuery(id));
            return Ok(employeeDto);
        }

        // GET api/HR/Employees/{id}/get-token
        [HttpGet("{id}/get-token")]
        public async Task<IActionResult> GetTokenEmployeeById([FromRoute] int id)
        {
            var employeeToken = await _mediator.Send(new GetTokenEmployeeByIdQuery(id));
            return Ok(employeeToken);
        }


        // POST api/HR/Employees
        [HttpPost]
        public async Task<IActionResult> CreateEmploye([FromBody] CreateEmployeeDto dto)
        {
            var command = _mapper.Map<CreateEmployeeCommand>(dto);

            int id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployeeById), new { id }, null);


        }

        // PUT api/HR/Employees
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmploye([FromRoute] int id, [FromBody] UpdateEmployeeDto dto)
        {
            var command = new UpdateEmployeeCommand(id);
            _mapper.Map(dto, command);
            await _mediator.Send(command);
            return Ok();
        }

        // DELETE api/HR/Employees
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmploye([FromRoute] int id)
        {
            await _mediator.Send(new DeleteEmployeeCommand(id));
            return NoContent();
        }
    }
}
