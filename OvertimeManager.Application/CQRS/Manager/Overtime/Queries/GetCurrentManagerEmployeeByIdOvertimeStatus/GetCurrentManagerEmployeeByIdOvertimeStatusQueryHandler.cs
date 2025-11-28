using MediatR;
using OvertimeManager.Application.CQRS.CommonCQRS;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeeByIdOvertimeStatus
{
    public class GetCurrentManagerEmployeeByIdOvertimeStatusQueryHandler : IRequestHandler<GetCurrentManagerEmployeeByIdOvertimeStatusQuery, EmployeeOvertimeStatusDto>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetCurrentManagerEmployeeByIdOvertimeStatusQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeOvertimeStatusDto> Handle(GetCurrentManagerEmployeeByIdOvertimeStatusQuery request, CancellationToken cancellationToken)
        {
            //var manager = await _employeeRepository.GetByIdAsync(request.CurrentManagerId);
            //if (manager == null)
            //    throw new NotFoundException("Manager not found", request.CurrentManagerId.ToString());
            //var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

            //if (employee == null || employee.ManagerId != manager.Id)
            //    throw new NotFoundException("Employee not found under the current manager", request.EmployeeId.ToString());

            var employee = await EmployeeHelper.GetEmployeeIfUnderManager(request.EmployeeId, request.CurrentManagerId, _employeeRepository);

            var statusDto = new EmployeeOvertimeStatusDto()
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