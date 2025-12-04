using AutoMapper;
using MediatR;
using OvertimeManager.Api.Controllers;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeesActiveCompensations
{
    public class GetCurrentManagerEmployeesActiveCompensationsQueryHanlder :
        IRequestHandler<GetCurrentManagerEmployeesActiveCompensationsQuery, PagedResult<GetCompensationDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompensationRepository _compensationRepository;
        private readonly IMapper _mapper;

        public GetCurrentManagerEmployeesActiveCompensationsQueryHanlder(IEmployeeRepository employeeRepository,
            ICompensationRepository compensationRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _compensationRepository = compensationRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<GetCompensationDto>> Handle(
            GetCurrentManagerEmployeesActiveCompensationsQuery request, CancellationToken cancellationToken)
        {
            var manager = await _employeeRepository.GetByIdAsync(request.CurrentManagerId);
            if (manager == null)
                throw new NotFoundException("Manager not found", request.CurrentManagerId.ToString());

            var (compensations, totalCount) = await _compensationRepository
                .GetAllActiveForEmployeesByManagerId(manager.Id, request.QueryOptions);
            var compensationsDto = _mapper.Map<IEnumerable<GetCompensationDto>>(compensations);

            var result = new PagedResult<GetCompensationDto>(compensationsDto, totalCount,
                request.QueryOptions.PageSize, request.QueryOptions.PageNumber);

            return result;
        }
    }
}