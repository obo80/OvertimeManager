using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OvertimeManager.Domain.Entities.User;
using OvertimeManager.Application.CQRS.Employee.Queries.GetAllEmployees;
using OvertimeManager.Application.CQRS.Employee.Queries.GetEmployeeById;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OvertimeManager.Api.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetAll ()
        {
            var employees = await _mediator.Send(new GetAllEmployeesQuery());
            return Ok(employees);
        }

        // GET api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
            return Ok(employee);
        }

        // POST api/Employees
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {


            return Created();
        }

        // PUT api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {

            return Created();
        }

        // DELETE api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return NoContent();
        }
    }
}
