namespace OvertimeManager.Application.CQRS.Employee.Overtime.DTOs
{
    public class CreateOvertimeDto
    {
        public string? Name { get; set; } = default;
        public string? BusinessJustificationReason { get; set; } = default;
        public double RequestedTime { get; set; }
    }
}
