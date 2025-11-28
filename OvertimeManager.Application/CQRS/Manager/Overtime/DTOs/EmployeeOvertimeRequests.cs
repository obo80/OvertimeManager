using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.DTOs
{
    public class EmployeeOvertimeRequestsDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeEmail { get; set; } = default!;
        public IEnumerable<GetOvertimeDto> OvertimeRequest { get; set; } = new List<GetOvertimeDto>();
    }
}
