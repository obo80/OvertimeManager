using OvertimeManager.Domain.Entities.User;

namespace OvertimeManager.Application.CQRS.Employee.Queries.GetAllEmployees
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public int? ManagerId { get; set; }
    }
}