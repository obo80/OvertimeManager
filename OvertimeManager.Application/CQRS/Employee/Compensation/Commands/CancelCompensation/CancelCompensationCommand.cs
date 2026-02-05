using MediatR;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Commands.CancelCompensation
{
    public class CancelCompensationCommand : IRequest
    {
        public CancelCompensationCommand(int currentEmployeeId, int id)
        {
            CurrentEmployeeId = currentEmployeeId;
            CompensationId = id;
        }

        public int CurrentEmployeeId { get; }
        public int CompensationId { get; }
    }
}