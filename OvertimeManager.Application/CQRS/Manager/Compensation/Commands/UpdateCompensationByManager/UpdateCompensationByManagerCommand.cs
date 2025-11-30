using MediatR;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Commands.UpdateCompensationByManager
{
    public class UpdateCompensationByManagerCommand : IRequest
    {
        public int CompensationId { get; }
        public int CurrentManagerId { get; }
        public int? RequestedForEmployeeId { get; set; }
        public double? RequestedTime { get; set; }
        public bool IsMultiplied { get; set; } = true; // Manager requests are multiplied

        public UpdateCompensationByManagerCommand(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            CompensationId = id;

        }


    }
}
