using OvertimeManager.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Entities.User
{
    public class Employee
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PasswordHash { get; set; }

        public int RoleId { get; set; } = 1;        //default role - Employee
        public virtual EmployeeRole Role { get; set; }

        public int? ManagerId { get; set; }
        public virtual Employee? Manager { get; set; }

        public virtual ICollection<Employee>? Subordinates { get; set; }

        public int OvertimeSummaryId { get; set; }
        public virtual EmployeeOvertimeSummary OvertimeSummary { get; set; }
    }
}
