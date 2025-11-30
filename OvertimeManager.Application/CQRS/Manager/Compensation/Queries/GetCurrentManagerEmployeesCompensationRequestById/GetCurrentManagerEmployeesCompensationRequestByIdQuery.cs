using MediatR;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeesCompensationRequestById
{
    public class GetCurrentManagerEmployeesCompensationRequestByIdQuery : IRequest<GetCompensationDto>
    {
        public GetCurrentManagerEmployeesCompensationRequestByIdQuery(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            CompensationId = id;
        }

        public int CurrentManagerId { get; }
        public int CompensationId { get; }
    }
}
