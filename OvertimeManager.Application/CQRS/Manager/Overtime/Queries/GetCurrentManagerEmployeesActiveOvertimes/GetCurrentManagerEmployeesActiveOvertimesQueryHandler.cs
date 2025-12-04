using AutoMapper;
using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeesActiveOvertimes
{
    class GetCurrentManagerEmployeesActiveOvertimesQueryHandler : 
        IRequestHandler<GetCurrentManagerEmployeesActiveOvertimesQuery, PagedResult<GetOvertimeDto>>
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
        public async Task<PagedResult<GetOvertimeDto>> Handle(
            GetCurrentManagerEmployeesActiveOvertimesQuery request, CancellationToken cancellationToken)
        {
            var manager = await _employeeRepository.GetByIdAsync(request.CurrentManagerId);
            if (manager == null)
                throw new NotFoundException("Manager not found", request.CurrentManagerId.ToString());

            var (overtimes, totalCount) = await _overtimeRepository
                .GetAllActiveForEmployeesByManagerId(manager.Id, request.QueryOptions);
            var overtimesDto = _mapper.Map<IEnumerable<GetOvertimeDto>>(overtimes);

            var result = new PagedResult<GetOvertimeDto>(overtimesDto, totalCount,
                request.QueryOptions.PageSize, request.QueryOptions.PageNumber);
            return result;
        }
    }
    


}