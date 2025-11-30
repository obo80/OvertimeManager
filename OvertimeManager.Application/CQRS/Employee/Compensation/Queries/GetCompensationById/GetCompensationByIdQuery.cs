using MediatR;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Queries.GetCompensationById
{
    public class GetCompensationByIdQuery : IRequest<GetCompensationDto>
    {
        public GetCompensationByIdQuery(int currentEmployeeId, int id)
        {
            CurrentEmployeeId = currentEmployeeId;
            CompensationId = id;
        }

        public int CurrentEmployeeId { get; }
        public int CompensationId { get; }
    }
}