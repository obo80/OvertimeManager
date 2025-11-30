using MediatR;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Commands.RejectCurrentManagerEmployeesOvertimeRequestById
{
    public class RejectCurrentManagerEmployeesOvertimeRequestByIdCommand : IRequest
    {


        public RejectCurrentManagerEmployeesOvertimeRequestByIdCommand(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            OvertimeId = id;
        }

        public int CurrentManagerId { get; }
        public int OvertimeId { get; }
    }
}