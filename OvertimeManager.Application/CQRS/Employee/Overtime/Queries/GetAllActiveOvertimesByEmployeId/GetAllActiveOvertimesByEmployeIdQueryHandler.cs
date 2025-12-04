using AutoMapper;
using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Api.Controllers
{
    class GetAllActiveOvertimesByEmployeIdQueryHandler : IRequestHandler<GetAllActiveOvertimesByEmployeIdQuery, PagedResult<GetOvertimeDto>>
    {
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly IMapper _mapper;
        public GetAllActiveOvertimesByEmployeIdQueryHandler(IOvertimeRepository overtimeRepository, IMapper mapper)
        {
            _overtimeRepository = overtimeRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<GetOvertimeDto>> Handle(GetAllActiveOvertimesByEmployeIdQuery request, CancellationToken cancellationToken)
        {
            var (overtimes, totalCount) = await _overtimeRepository.GetAllActiveForEmployeeIdAsync(request.EmployeeId, request.QueryOptions);
            var overtimesDto = _mapper.Map<List<GetOvertimeDto>>(overtimes);


            var result = new PagedResult<GetOvertimeDto>(overtimesDto, totalCount, request.QueryOptions.PageSize, request.QueryOptions.PageNumber);
            return result;
        }
    }
}