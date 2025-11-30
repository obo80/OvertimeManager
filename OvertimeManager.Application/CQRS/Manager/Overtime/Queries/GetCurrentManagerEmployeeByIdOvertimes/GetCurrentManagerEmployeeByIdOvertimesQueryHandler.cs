using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.CommonCQRS;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeeByIdOvertimes
{
    class GetCurrentManagerEmployeeByIdOvertimesQueryHandler : IRequestHandler<GetCurrentManagerEmployeeByIdOvertimesQuery, IEnumerable<GetOvertimeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly IMapper _mapper;

        public GetCurrentManagerEmployeeByIdOvertimesQueryHandler(IEmployeeRepository employeeRepository,
            IOvertimeRepository overtimeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _overtimeRepository = overtimeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetOvertimeDto>> Handle(GetCurrentManagerEmployeeByIdOvertimesQuery request, CancellationToken cancellationToken)
        {
            var employee = await EmployeeHelper.GetEmployeeIfUnderManager(request.EmployeeId, request.CurrentManagerId, _employeeRepository);

            var overtimes = await _overtimeRepository.GetAllForEmployeeIdAsync(employee.Id);
            return _mapper.Map<IEnumerable<GetOvertimeDto>>(overtimes);

        }
    }


}