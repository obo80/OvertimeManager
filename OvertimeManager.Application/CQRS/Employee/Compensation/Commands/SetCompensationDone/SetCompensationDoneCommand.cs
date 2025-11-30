using MediatR;

namespace OvertimeManager.Api.Controllers
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