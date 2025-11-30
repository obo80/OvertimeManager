using MediatR;

namespace OvertimeManager.Application.CQRS.HR.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? RoleId { get; set; }
        public int? ManagerId { get; set; }

        public UpdateEmployeeCommand(int id)
        {
            Id = id;
        }

    }
}
