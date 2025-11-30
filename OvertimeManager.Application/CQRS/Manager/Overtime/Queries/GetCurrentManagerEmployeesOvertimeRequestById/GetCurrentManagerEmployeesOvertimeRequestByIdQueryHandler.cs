using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.CommonCQRS;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeesOvertimeRequestById
{
    public class GetCurrentManagerEmployeesOvertimeRequestByIdQueryHandler 
        : IRequestHandler<GetCurrentManagerEmployeesOvertimeRequestByIdQuery, GetOvertimeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly IMapper _mapper;

        public GetCurrentManagerEmployeesOvertimeRequestByIdQueryHandler(IEmployeeRepository employeeRepository, 
            IOvertimeRepository overtimeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _overtimeRepository = overtimeRepository;
            _mapper = mapper;
        }
        public async Task<GetOvertimeDto> Handle(GetCurrentManagerEmployeesOvertimeRequestByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var overtime = await _overtimeRepository.GetByIdAsync(request.OvertimeId);

            if (overtime == null || !await EmployeeHelper.IsManagerEmployeeRequest(overtime, 
                    request.OvertimeId, request.CurrentManagerId, _employeeRepository))
                throw new UnauthorizedException("You are not authorized to get this overtime request."); 


            var getOvertimeDto = _mapper.Map<GetOvertimeDto>(overtime);
            return getOvertimeDto;
        }
    }
}