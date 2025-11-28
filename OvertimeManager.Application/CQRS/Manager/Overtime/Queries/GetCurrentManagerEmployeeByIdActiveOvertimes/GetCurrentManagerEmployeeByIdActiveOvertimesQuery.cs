using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.CommonCQRS;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeeByIdActiveOvertimes
{
    public class GetCurrentManagerEmployeeByIdActiveOvertimesQuery : IRequest<IEnumerable<GetOvertimeDto>>
    {
        public GetCurrentManagerEmployeeByIdActiveOvertimesQuery(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            EmployeeId = id;
        }

        public int CurrentManagerId { get; }
        public int EmployeeId { get; }
    }

    class GetCurrentManagerEmployeeByIdActiveOvertimesQueryHandler : IRequestHandler<GetCurrentManagerEmployeeByIdActiveOvertimesQuery, IEnumerable<GetOvertimeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly IMapper _mapper;
        public GetCurrentManagerEmployeeByIdActiveOvertimesQueryHandler(IEmployeeRepository employeeRepository,
            IOvertimeRepository overtimeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _overtimeRepository = overtimeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetOvertimeDto>> Handle(GetCurrentManagerEmployeeByIdActiveOvertimesQuery request, CancellationToken cancellationToken)
        {
            //var manager = await _employeeRepository.GetByIdAsync(request.CurrentManagerId);
            //if (manager == null)
            //    throw new NotFoundException("Manager not found", request.CurrentManagerId.ToString());
            //var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

            //if (employee == null || employee.ManagerId != manager.Id)
            //    throw new NotFoundException("Employee not found under the current manager", request.EmployeeId.ToString());

            var employee = await EmployeeHelper.GetEmployeeIfUnderManager(request.EmployeeId, request.CurrentManagerId, _employeeRepository);

            var overtimes = await _overtimeRepository.GetAllActiveForEmployeeIdAsync(employee.Id);
            return _mapper.Map<IEnumerable<GetOvertimeDto>>(overtimes);
        }
    }



}