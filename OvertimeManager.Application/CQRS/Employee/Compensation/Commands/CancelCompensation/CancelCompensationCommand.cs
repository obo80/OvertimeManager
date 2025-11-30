using MediatR;

namespace OvertimeManager.Api.Controllers
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