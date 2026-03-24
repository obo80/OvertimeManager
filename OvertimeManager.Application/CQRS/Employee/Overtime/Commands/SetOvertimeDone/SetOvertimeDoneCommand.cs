using MediatR;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Commands.SetOvertimeDone
{
    public class SetOvertimeDoneCommand : IRequest
    {
        public double? ActualTime { get; set; }

        public int CurrentEmployeeId { get; }
        public int OvertimeId { get; }

        public SetOvertimeDoneCommand(int currentEmployeeId, int id)
        {
            CurrentEmployeeId = currentEmployeeId;
            OvertimeId = id;
        }

    }
}