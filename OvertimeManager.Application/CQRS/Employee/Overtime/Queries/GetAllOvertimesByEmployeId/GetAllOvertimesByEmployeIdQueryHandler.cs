using AutoMapper;
using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Queries.GetAllOvertimesByEmployeId
{
    public class GetAllOvertimesByEmployeIdQueryHandler : IRequestHandler<GetAllOvertimesByEmployeIdQuery, PagedResult<GetOvertimeDto>>
    {
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly IMapper _mapper;

        public GetAllOvertimesByEmployeIdQueryHandler(IOvertimeRepository overtimeRepository, IMapper mapper)
        {
            _overtimeRepository = overtimeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<GetOvertimeDto>> Handle(GetAllOvertimesByEmployeIdQuery request, CancellationToken cancellationToken)
        {
            var (overtimes, totalCount) = await _overtimeRepository.GetAllForEmployeeIdAsync(request.EmployeeId, request.QueryOptions);
            var overtimesDto = _mapper.Map<List<GetOvertimeDto>>(overtimes);

            var result = new PagedResult<GetOvertimeDto>(overtimesDto, totalCount, request.QueryOptions.PageSize, request.QueryOptions.PageNumber);
            return result;
        }
    }
}
