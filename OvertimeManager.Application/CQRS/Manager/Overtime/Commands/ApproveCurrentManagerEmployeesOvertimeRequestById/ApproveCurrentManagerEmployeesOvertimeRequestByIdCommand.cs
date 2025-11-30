using MediatR;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Commands.ApproveCurrentManagerEmployeesOvertimeRequestById
{
    public class ApproveCurrentManagerEmployeesOvertimeRequestByIdCommand : IRequest
    {


        public ApproveCurrentManagerEmployeesOvertimeRequestByIdCommand(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            OvertimeId = id;
        }

        public int CurrentManagerId { get; }
        public int OvertimeId { get; }
    }
}