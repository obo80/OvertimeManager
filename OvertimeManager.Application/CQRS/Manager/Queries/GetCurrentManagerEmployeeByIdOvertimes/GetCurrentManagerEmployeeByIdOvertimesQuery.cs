using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Entities.User;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Api.Controllers
{
    public class GetCurrentManagerEmployeeByIdOvertimesQuery : IRequest<IEnumerable<GetOvertimeDto>>
    {
        public GetCurrentManagerEmployeeByIdOvertimesQuery(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            EmployeeId = id;
        }

        public int CurrentManagerId { get; }
        public int EmployeeId { get; }
    }

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
            var manager = await _employeeRepository.GetByIdAsync(request.CurrentManagerId);
            if (manager == null)
                throw new NotFoundException("Manager not found", request.CurrentManagerId.ToString());
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

            if (employee == null || employee.ManagerId != manager.Id)
                throw new NotFoundException("Employee not found under the current manager", request.EmployeeId.ToString());

            var overtimes = await _overtimeRepository.GetAllForEmployeeIdAsync(employee.Id);
            return _mapper.Map<IEnumerable<GetOvertimeDto>>(overtimes);

        }
    }


}