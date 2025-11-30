using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeeByIdOvertimes
{
    public class GetCurrentManagerEmployeeByIdOvertimesQuery : IRequest<IEnumerable<GetOvertimeDto>>
    {
        public GetCurrentManagerEmployeeByIdOvertimesQuery(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            EmployeeId = id;
        }

        public int CurrentManagerId { get; }
        public int EmployeeId { get; }
    }


}