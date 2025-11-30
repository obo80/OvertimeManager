using MediatR;
using OvertimeManager.Domain.Constants;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Commands.CreateOvertime
{
    public class CreateOvertimeCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; } = default;
        public string? BusinessJustificationReason { get; set; } = default;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateOnly CreatedForDay { get; set; } = DateOnly.FromDateTime(DateTime.Now); //default to today
        public int RequestedByEmployeeId { get; set; }
        public int RequestedForEmployeeId { get; set; }

        public double RequestedTime { get; set; }
        public string Status { get; set; } = ((StatusEnum)StatusEnum.Pending).ToString();

        public CreateOvertimeCommand(int currentEmployeeId)
        {
            RequestedByEmployeeId = currentEmployeeId;
            RequestedForEmployeeId = currentEmployeeId;
        }
    }
}
