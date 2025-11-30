using MediatR;
using OvertimeManager.Domain.Constants;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Commands.CreateCompensationByManager
{
    public class CreateCompensationByManagerCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateOnly CreatedForDay { get; set; } = DateOnly.FromDateTime(DateTime.Now); //default to today
        public int RequestedByEmployeeId { get; set; }
        public int RequestedForEmployeeId { get; set; }

        public double RequestedTime { get; set; }
        public string Status { get; set; } = ((StatusEnum)StatusEnum.Approved).ToString();
        public DateTime ApprovedAt { get; set; } =  DateTime.Now;
        public int ApprovedByEmployeeId { get; set; } 
        public string approvedByEmployeeEmail { get; set; }

        public bool IsMultiplied { get; set; } = true; // Manager requests are multiplied

        public CreateCompensationByManagerCommand(int currentManagerId)
        {
            RequestedByEmployeeId = currentManagerId;
            ApprovedByEmployeeId = currentManagerId;
        }
    }

}
