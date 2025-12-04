using MediatR;
using OvertimeManager.Domain.Constants;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Commands.CreateCompensationByManager
{
    public class CreateCompensationByManagerCommand(int currentManagerId, int employeeId) : IRequest<int>
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateOnly CreatedForDay { get; set; } = DateOnly.FromDateTime(DateTime.Now); //default to today
        public int RequestedByEmployeeId { get; set; } = currentManagerId;
        public int RequestedForEmployeeId { get; set; } = employeeId;

        public double RequestedTime { get; set; }
        public string Status { get; set; } = ((StatusEnum)StatusEnum.Approved).ToString();
        public DateTime ApprovedAt { get; set; } =  DateTime.Now;
        public int ApprovedByEmployeeId { get; set; } = currentManagerId;
        public string? ApprovedByEmployeeEmail { get; set; }

        public bool IsMultiplied { get; set; } = true; // Manager requests are multiplied
    }

}
