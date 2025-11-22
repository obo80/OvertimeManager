using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OvertimeManager.Application.CQRS.Employee.Commands.CreateEmployee;
using OvertimeManager.Application.CQRS.Employee.Commands.DeleteEmployee;
using OvertimeManager.Application.CQRS.Employee.Commands.UpdateEmployee;
using OvertimeManager.Application.CQRS.Employee.Queries.GetAllEmployees;
using OvertimeManager.Application.CQRS.Employee.Queries.GetEmployeeById;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OvertimeManager.Api.Controllers
{
    [Route("api/Employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EmployeesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees ()
        {
            var employees = await _mediator.Send(new GetAllEmployeesQuery());
            return Ok(employees);
        }

        // GET api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
            return Ok(employee);
        }

        // POST api/Employees
        [HttpPost]
        public async Task<IActionResult> CreateEmploye([FromBody] CreateEmployeeCommand command)
        {
            int id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployeeById), new { id }, null);
            //return Created();
        }

        // PUT api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmploye([FromRoute] int id, [FromBody] UpdateEmployeeCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return Ok();
        }

        // DELETE api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmploye([FromRoute] int id)
        {
            await _mediator.Send(new DeleteEmployeeCommand(id));
            return NoContent();
        }
    }
}
