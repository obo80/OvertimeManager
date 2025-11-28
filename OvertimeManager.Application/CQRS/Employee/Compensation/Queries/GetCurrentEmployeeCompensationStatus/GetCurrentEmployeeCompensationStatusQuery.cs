using MediatR;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Queries.GetCurrentEmployeeCompensationStatus
{
    public class GetCurrentEmployeeCompensationStatusQuery : IRequest<EmployeeCompensationStatusDto>
    {
        public int CurrentEmployeeId { get; }
        public GetCurrentEmployeeCompensationStatusQuery(int currentEmployeeId)
        {
            CurrentEmployeeId = currentEmployeeId;
        }
    }

    class GetCurrentEmployeeCompensationStatusQueryHandler : IRequestHandler<GetCurrentEmployeeCompensationStatusQuery, EmployeeCompensationStatusDto>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetCurrentEmployeeCompensationStatusQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeCompensationStatusDto> Handle(GetCurrentEmployeeCompensationStatusQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.CurrentEmployeeId);
            if (employee == null)
                throw new NotFoundException("Employee not found", request.CurrentEmployeeId.ToString());

            var statusDto = new EmployeeCompensationStatusDto()
            {
                EmployeeId = employee.Id,
                EmployeeEmail = employee.Email,
                TakenOvertime = employee.OvertimeSummary.TakenOvertime,
                SettledOvertime = employee.OvertimeSummary.SettledOvertime,
                UnsettledOvertime = employee.OvertimeSummary.UnsettledOvertime
            };
            return statusDto;
        }
    }
}

