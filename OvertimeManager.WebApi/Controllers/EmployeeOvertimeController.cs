using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OvertimeManager.Api.Controllers
{
    [Route("api/Employee/Overtime")]
    [ApiController]
    public class EmployeeOvertimeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("EmployeeOvertimeController is working.");
        }
    }
}
