using MediatR;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeeByIdCompensationStatus
{
    public class GetCurrentManagerEmployeeByIdCompensationStatusQuery : IRequest<EmployeeCompensationStatusDto>
    {
        public GetCurrentManagerEmployeeByIdCompensationStatusQuery(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            EmployeeId = id;
        }

        public int CurrentManagerId { get; }
        public int EmployeeId { get; }
    }

    public class GetCurrentManagerEmployeeByIdCompensationStatusQueryHandler : IRequestHandler<GetCurrentManagerEmployeeByIdCompensationStatusQuery, EmployeeCompensationStatusDto>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetCurrentManagerEmployeeByIdCompensationStatusQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeCompensationStatusDto> Handle(GetCurrentManagerEmployeeByIdCompensationStatusQuery request, CancellationToken cancellationToken)
        {
            var manager = await _employeeRepository.GetByIdAsync(request.CurrentManagerId);
            if (manager == null)
                throw new NotFoundException("Manager not found", request.CurrentManagerId.ToString());
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

            if (employee == null || employee.ManagerId != manager.Id)
                throw new NotFoundException("Employee not found under the current manager", request.EmployeeId.ToString());

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