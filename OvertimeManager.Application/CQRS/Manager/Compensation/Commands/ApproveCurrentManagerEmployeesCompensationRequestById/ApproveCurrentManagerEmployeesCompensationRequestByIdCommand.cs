using MediatR;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Commands.ApproveCurrentManagerEmployeesCompensationRequestById
{
    public class ApproveCurrentManagerEmployeesCompensationRequestByIdCommand : IRequest
    {
        public ApproveCurrentManagerEmployeesCompensationRequestByIdCommand(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            CompensationId = id;
        }

        public int CurrentManagerId { get; }
        public int CompensationId { get; }
    }
}