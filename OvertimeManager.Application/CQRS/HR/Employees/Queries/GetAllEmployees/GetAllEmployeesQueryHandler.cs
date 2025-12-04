using AutoMapper;
using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Application.CQRS.HR.Employees.DTOs;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.HR.Employees.Queries.GetAllEmployees
{
    internal class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, PagedResult<HREmployeeWithOvetimeDataDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<HREmployeeWithOvetimeDataDto>> Handle(GetAllEmployeesQuery request, 
            CancellationToken cancellationToken)
        {
            var (employees,totalCount) = await _employeeRepository.GetAllAsync(request.QueryOptions);
            var dtos = _mapper.Map<IEnumerable<HREmployeeWithOvetimeDataDto>>(employees);

            var result = new PagedResult<HREmployeeWithOvetimeDataDto>(dtos, totalCount, request.QueryOptions.PageSize, 
                request.QueryOptions.PageNumber);
            return result;
        }
    }
}
