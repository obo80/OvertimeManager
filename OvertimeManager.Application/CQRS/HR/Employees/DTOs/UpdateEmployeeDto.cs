namespace OvertimeManager.Application.CQRS.HR.Employees.DTOs
{
    public class UpdateEmployeeDto
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? RoleId { get; set; }
        public int? ManagerId { get; set; }
    }
}
