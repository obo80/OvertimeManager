using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.Employee.Account.DTOs
{
    public class SetPassowordDto
    {
        public string? Email { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmedPassword { get; set; }
    }
}
