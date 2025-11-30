using MediatR;

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
