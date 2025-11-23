using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.HR.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest
    {
        public int Id { get; }
        public DeleteEmployeeCommand(int id)
        {
            Id = id;
        }

        
    }
}
