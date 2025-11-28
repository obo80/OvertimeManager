using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Api.Controllers
{
    public class EmployeeCompensationRequestsDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeEmail { get; set; } = default!;
        public IEnumerable<GetCompensationDto> CompensationRequest { get; set; } = new List<GetCompensationDto>();
    }
}