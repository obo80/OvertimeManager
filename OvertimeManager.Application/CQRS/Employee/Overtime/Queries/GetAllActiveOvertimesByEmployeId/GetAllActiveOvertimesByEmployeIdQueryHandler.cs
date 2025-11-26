using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Api.Controllers
{
    class GetAllActiveOvertimesByEmployeIdQueryHandler : IRequestHandler<GetAllActiveOvertimesByEmployeIdQuery, IEnumerable<GetOvertimeDto>>
    {
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly IMapper _mapper;
        public GetAllActiveOvertimesByEmployeIdQueryHandler(IOvertimeRepository overtimeRepository, IMapper mapper)
        {
            _overtimeRepository = overtimeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetOvertimeDto>> Handle(GetAllActiveOvertimesByEmployeIdQuery request, CancellationToken cancellationToken)
        {
            var overtimes = await _overtimeRepository.GetAllActiveForEmployeeIdAsync(request.EmployeeId);
            var overtimesDto = _mapper.Map<List<GetOvertimeDto>>(overtimes);
            return overtimesDto;
        }
    }
}