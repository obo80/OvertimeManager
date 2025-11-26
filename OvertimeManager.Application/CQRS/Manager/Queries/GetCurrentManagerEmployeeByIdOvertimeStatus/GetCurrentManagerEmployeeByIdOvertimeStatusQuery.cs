using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;

namespace OvertimeManager.Api.Controllers
{
    public class GetCurrentManagerEmployeeByIdOvertimeStatusQuery : IRequest<EmployeeOvertimeStatusDto>
    {
        public GetCurrentManagerEmployeeByIdOvertimeStatusQuery(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            EmployeeId = id;
        }

        public int CurrentManagerId { get; }
        public int EmployeeId { get; }
    }
}