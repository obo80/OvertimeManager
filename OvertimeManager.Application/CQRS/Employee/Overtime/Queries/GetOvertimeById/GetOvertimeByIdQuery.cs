using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Queries.GetOvertimeById
{
    public class GetOvertimeByIdQuery : IRequest<GetOvertimeDto>
    {
        public int CurrentEmployeeId { get; }
        public int OvertimeId { get; }

        public GetOvertimeByIdQuery(int currentEmployeeId, int overtimeId)
        {
            CurrentEmployeeId = currentEmployeeId;
            OvertimeId = overtimeId;
        }
    }
}
