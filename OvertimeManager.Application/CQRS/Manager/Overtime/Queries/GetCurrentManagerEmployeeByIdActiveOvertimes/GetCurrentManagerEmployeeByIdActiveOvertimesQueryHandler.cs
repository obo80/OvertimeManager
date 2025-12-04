using AutoMapper;
using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.CommonCQRS;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Queries.GetCurrentManagerEmployeeByIdActiveOvertimes
{
    class GetCurrentManagerEmployeeByIdActiveOvertimesQueryHandler : 
        IRequestHandler<GetCurrentManagerEmployeeByIdActiveOvertimesQuery, PagedResult<GetOvertimeDto>>
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
        public async Task<PagedResult<GetOvertimeDto>> Handle(
            GetCurrentManagerEmployeeByIdActiveOvertimesQuery request, CancellationToken cancellationToken)
        {
            var employee = await EmployeeHelper.GetEmployeeIfUnderManager(
                request.EmployeeId, request.CurrentManagerId, _employeeRepository);

            var (overtimes, totalCount) = await _overtimeRepository.GetAllActiveForEmployeeIdAsync(
                employee.Id, request.QueryOptions);

            var overtimesDto = _mapper.Map<List<GetOvertimeDto>>(overtimes);

            var result = new PagedResult<GetOvertimeDto>(
                overtimesDto, totalCount, request.QueryOptions.PageSize, request.QueryOptions.PageNumber);

            return result;
        }
    }



}