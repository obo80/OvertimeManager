using MediatR;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Commands.SetCompensationDone
{
    public class SetCompensationDoneCommand : IRequest
    {
        public int CurrentEmployeeId { get; }
        public int CompensationId { get; }

        public SetCompensationDoneCommand(int currentEmployeeId, int id)
        {
            CurrentEmployeeId = currentEmployeeId;
            CompensationId = id;
        }
    }
}