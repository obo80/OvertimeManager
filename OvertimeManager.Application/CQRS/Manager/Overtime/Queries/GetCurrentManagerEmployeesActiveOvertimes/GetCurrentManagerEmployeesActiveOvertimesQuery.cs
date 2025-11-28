using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Application.CQRS.Manager.Overtime.DTOs;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeesActiveOvertimes
{
    public class GetCurrentManagerEmployeesActiveOvertimesQuery : IRequest<IEnumerable<EmployeeOvertimeRequestsDto>>
    {
        public GetCurrentManagerEmployeesActiveOvertimesQuery(int currentManagerId)
        {
            CurrentManagerId = currentManagerId;
        }

        public int CurrentManagerId { get; }
    }

    class GetCurrentManagerEmployeesActiveOvertimesQueryHandler : IRequestHandler<GetCurrentManagerEmployeesActiveOvertimesQuery, IEnumerable<EmployeeOvertimeRequestsDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly IMapper _mapper;

        public GetCurrentManagerEmployeesActiveOvertimesQueryHandler(IEmployeeRepository employeeRepository,
            IOvertimeRepository overtimeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _overtimeRepository = overtimeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeOvertimeRequestsDto>> Handle(GetCurrentManagerEmployeesActiveOvertimesQuery request, CancellationToken cancellationToken)
        {
            var manager = await _employeeRepository.GetByIdAsync(request.CurrentManagerId);
            if (manager == null)
                throw new NotFoundException("Manager not found", request.CurrentManagerId.ToString());

            var employees = await _employeeRepository.GetAllByManagerIdAsync(manager.Id);

            var employeeOvertimeRequestsDtos = new List<EmployeeOvertimeRequestsDto>();

            foreach (var employee in employees)
            {
                var overtimes = await _overtimeRepository.GetAllActiveForEmployeeIdAsync(employee.Id);
                var overtimesDto = _mapper.Map<List<GetOvertimeDto>>(overtimes);
                employeeOvertimeRequestsDtos.Add(new EmployeeOvertimeRequestsDto
                {
                    EmployeeId = employee.Id,
                    EmployeeEmail = employee.Email,
                    OvertimeRequest = overtimesDto
                });
            }

            return employeeOvertimeRequestsDtos;
        }
    }
    


}