using AutoMapper;
using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.CommonCQRS;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeeByIdActiveCompensations
{
    public class GetCurrentManagerEmployeeByIdActiveCompensationsQueryHandler :
        IRequestHandler<GetCurrentManagerEmployeeByIdActiveCompensationsQuery, PagedResult<GetCompensationDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompensationRepository _compensationRepository;
        private readonly IMapper _mapper;

        public GetCurrentManagerEmployeeByIdActiveCompensationsQueryHandler(IEmployeeRepository employeeRepository,
            ICompensationRepository compensationRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _compensationRepository = compensationRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<GetCompensationDto>> Handle(
            GetCurrentManagerEmployeeByIdActiveCompensationsQuery request, CancellationToken cancellationToken)
        {
            var employee = await EmployeeHelper.GetEmployeeIfUnderManager(request.EmployeeId, request.CurrentManagerId, _employeeRepository);
            var (compensations, totalCount) = await _compensationRepository.GetAllActiveForEmployeeIdAsync(employee.Id, request.QueryOptions);
            var compensationsDto = _mapper.Map<IEnumerable<GetCompensationDto>>(compensations);

            var result = new PagedResult<GetCompensationDto>(compensationsDto, totalCount, request.QueryOptions.PageSize, request.QueryOptions.PageNumber);
            return result;
        }
    }
}