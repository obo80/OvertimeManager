using MediatR;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Commands.UpdateOvertime
{
    public class UpdateOvertimeCommand : IRequest
    {
        public string? Name { get; set; }
        public string? BusinessJustificationReason { get; set; }
        public double? RequestedTime { get; set; }
        public int CurrentEmployeeId { get; }
        public int OvertimeId { get; }
        public UpdateOvertimeCommand(int currentEmployeeId, int overtimeId)
        {
            CurrentEmployeeId = currentEmployeeId;
            OvertimeId = overtimeId;
        }


    }



}
