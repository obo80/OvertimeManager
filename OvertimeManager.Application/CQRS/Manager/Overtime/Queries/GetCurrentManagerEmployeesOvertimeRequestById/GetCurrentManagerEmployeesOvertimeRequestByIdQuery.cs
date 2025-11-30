using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeesOvertimeRequestById
{
    public class GetCurrentManagerEmployeesOvertimeRequestByIdQuery : IRequest<GetOvertimeDto>
    {
        public GetCurrentManagerEmployeesOvertimeRequestByIdQuery(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            OvertimeId = id;
        }

        public int CurrentManagerId { get; }
        public int OvertimeId { get; }
    }
}