using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Queries.GetAllOvertimesByEmployeId
{
    public class GetAllOvertimesByEmployeIdQueryHandler : IRequestHandler<GetAllOvertimesByEmployeIdQuery, IEnumerable<GetOvertimeDto>>
    {
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly IMapper _mapper;

        public GetAllOvertimesByEmployeIdQueryHandler(IOvertimeRepository overtimeRepository, IMapper mapper)
        {
            _overtimeRepository = overtimeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetOvertimeDto>> Handle(GetAllOvertimesByEmployeIdQuery request, CancellationToken cancellationToken)
        {
            var overtimes = await _overtimeRepository.GetAllForEmployeeIdAsync(request.EmployeeId);
            var overtimesDto = _mapper.Map<List<GetOvertimeDto>>(overtimes);

            return overtimesDto;
        }
    }
}
