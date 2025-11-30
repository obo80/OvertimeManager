using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeeByIdActiveOvertimes
{
    public class GetCurrentManagerEmployeeByIdActiveOvertimesQuery : IRequest<IEnumerable<GetOvertimeDto>>
    {
        public GetCurrentManagerEmployeeByIdActiveOvertimesQuery(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            EmployeeId = id;
        }

        public int CurrentManagerId { get; }
        public int EmployeeId { get; }
    }



}