using MediatR;

namespace OvertimeManager.Application.CQRS.HR.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<int>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; } = 1; //default role for Employee
        public int? ManagerId { get; set; }


    }
}
