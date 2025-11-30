using MediatR;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Commands.RejectCurrentManagerEmployeesCompensationRequestById
{
    public class RejectCurrentManagerEmployeesCompensationRequestByIdCommand : IRequest
    {
        public RejectCurrentManagerEmployeesCompensationRequestByIdCommand(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            CompensationId = id;
        }

        public int CurrentManagerId { get; }
        public int CompensationId { get; }
    }
}
