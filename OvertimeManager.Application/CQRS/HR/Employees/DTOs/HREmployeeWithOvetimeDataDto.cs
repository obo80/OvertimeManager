namespace OvertimeManager.Application.CQRS.HR.Employees.DTOs
{
    public class HREmployeeWithOvetimeDataDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public int? ManagerId { get; set; } 

        public bool MustChangePassword { get; set; }
        public double TakenOvertime { get; set; }
        public double SettledOvertime { get; set; }
        public double UnsettledOvertime { get; set; }    
    }
}
