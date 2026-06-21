namespace OvertimeManager.Application.CQRS.HR.Employees.DTOs
{
    public class HREmployeeWithOvetimeDataDto
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int RoleId { get; set; }
        public int? ManagerId { get; set; } 

        public bool MustChangePassword { get; set; }
        public double TakenOvertime { get; set; }
        public double SettledOvertime { get; set; }
        public double UnsettledOvertime { get; set; }    
    }
}
