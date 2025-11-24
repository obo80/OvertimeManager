using MediatR;
using OvertimeManager.Application.Common;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Queries.GetCurrentEmployeeOvertimeStatus
{
    public class GetCurrentEmployeeOvertimeStatusQuery : IRequest<EmployeeOvertimeStatusDto>
    {

        public int CurrentEmployeeId { get; }

        public GetCurrentEmployeeOvertimeStatusQuery(int currentEmployeeId)
        {
            CurrentEmployeeId = currentEmployeeId;
        }
    }

    public class GetCurrentEmployeeOvertimeStatusQueryHandler : IRequestHandler<GetCurrentEmployeeOvertimeStatusQuery, EmployeeOvertimeStatusDto>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetCurrentEmployeeOvertimeStatusQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeOvertimeStatusDto> Handle(GetCurrentEmployeeOvertimeStatusQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.CurrentEmployeeId);
            if (employee == null) 
                throw new NotFoundException("Employee not found", request.CurrentEmployeeId.ToString());

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
