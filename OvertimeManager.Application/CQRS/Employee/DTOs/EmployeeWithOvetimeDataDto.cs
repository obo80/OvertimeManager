using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.Employee.DTOs
{
    public class EmployeeWithOvetimeDataDto : EmployeeDto
    {
        public double TakenOvertime { get; set; }
        public double SettledOvertimet { get; set; }
        public double UnsetledOvertime { get; set; }
    }
}
