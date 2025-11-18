using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Infrastructure.Persistence;

namespace OvertimeManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeRequestsController : ControllerBase
    {
        private readonly OvertimeManagerDbContext _context;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        //public OvertimeRequestsController(IMediator mediator, IMapper mapper)
        //{
        //    this.mediator = mediator;
        //    this.mapper = mapper;
        //}

        public OvertimeRequestsController(OvertimeManagerDbContext context)
        {
            _context = context;
        }

        // GET: api/OvertimeRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OvertimeRequest>>> GetOvertimeRequests()
        {
            return Ok("Testowy OKejek");
            //return await _context.OvertimeRequests.ToListAsync();
        }

        // GET: api/OvertimeRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OvertimeRequest>> GetOvertimeRequest(int id)
        {
            var overtimeRequest = await _context.OvertimeRequests.FindAsync(id);

            if (overtimeRequest == null)
            {
                return NotFound();
            }

            return overtimeRequest;
        }

        // PUT: api/OvertimeRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOvertimeRequest(int id, OvertimeRequest overtimeRequest)
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

        // POST: api/OvertimeRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OvertimeRequest>> PostOvertimeRequest(OvertimeRequest overtimeRequest)
        {
            _context.OvertimeRequests.Add(overtimeRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOvertimeRequest", new { id = overtimeRequest.Id }, overtimeRequest);
        }

        // DELETE: api/OvertimeRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOvertimeRequest(int id)
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
