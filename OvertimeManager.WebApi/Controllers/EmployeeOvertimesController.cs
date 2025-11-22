using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OvertimeManager.Application.CQRS.Employee.Commands.CreateEmployee;
using OvertimeManager.Application.CQRS.OvertimeRequest.Commands.CreateOvertimeRequest;
using OvertimeManager.Application.CQRS.OvertimeRequest.DTOs;
using OvertimeManager.Application.CQRS.OvertimeRequest.Queries.GetAllMyOvertimeRequests;
using OvertimeManager.Application.CQRS.OvertimeRequest.Queries.GetOvertimeRequestById;
using OvertimeManager.Domain.Entities.Overtime;
using System.Threading.Tasks;

namespace OvertimeManager.Api.Controllers
{
    [Route("api/Employee/Overtime")]
    [ApiController]
    public class EmployeeOvertimesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EmployeeOvertimesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }



        // GET: api/Employee/Overtime
        [HttpGet]
        public async Task<IActionResult> GetAllMyOvertimeRequests([FromHeader] string authorizationToken)
        {
            var myRequests = await _mediator.Send(new GetAllMyOvertimeRequestsQuery(authorizationToken));
            return Ok(myRequests);
        }

        // GET: api/Employee/Overtime/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOvertimeRequest(int id, [FromHeader] string authorizationToken)
        {
            var overtimeRequest = await _mediator.Send(new GetOvertimeRequestByIdQuery(id));

            if (overtimeRequest == null)
            {
                return NotFound();
            }
            return Ok(overtimeRequest);
        }


        // POST: api/Employee/Overtime
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateOvertimeRequest([FromBody] CreateOvertimeRequestCommand command, [FromHeader] string authorizationToken)
        {
            int id = await _mediator.Send(command);// do poprawienia

            return CreatedAtAction("GetOvertimeRequest", new { id }, null);
        }

        // PUT: api/Employee/Overtime/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOvertimeRequest(int id, OvertimeRequest overtimeRequest, [FromHeader] string authorizationToken)
        {
            if (id != overtimeRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(overtimeRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OvertimeRequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // DELETE: api/EmployeeOvertime/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOvertimeRequest(int id, [FromHeader] string authorization)
        {
            var overtimeRequest = await _context.OvertimeRequests.FindAsync(id);
            if (overtimeRequest == null)
            {
                return NotFound();
            }

            _context.OvertimeRequests.Remove(overtimeRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OvertimeRequestExists(int id)
        {
            return _context.OvertimeRequests.Any(e => e.Id == id);
        }
    }
}
