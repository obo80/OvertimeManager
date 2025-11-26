using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Api.Controllers
{
    public class GetCurrentManagerEmployeesOvertimeRequestByIdQuery : IRequest<GetOvertimeDto>
    {
        public GetCurrentManagerEmployeesOvertimeRequestByIdQuery(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            OvertimeId = id;
        }

        public int CurrentManagerId { get; }
        public int OvertimeId { get; }
    }

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
            if (overtime == null)
                throw new NotFoundException("Overtime request not found.", request.OvertimeId.ToString());

            var manager = await _employeeRepository.GetByIdAsync(request.CurrentManagerId);
            if (manager == null)
                throw new NotFoundException("Manager not found", request.CurrentManagerId.ToString());

            if (overtime.RequestedForEmployee!.ManagerId != manager.Id)
                throw new UnauthorizedException("You are not authorized to get this overtime request."); 


            var getOvertimeDto = _mapper.Map<GetOvertimeDto>(overtime);
            return getOvertimeDto;
        }
    }
}