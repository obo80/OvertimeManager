using AutoMapper;
using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Queries.GetAllActiveCompensationsByEmployeId
{
    public class GetAllActiveCompensationsByEmployeIdQueryHandler : IRequestHandler<GetAllActiveCompensationsByEmployeIdQuery, PagedResult<GetCompensationDto>>
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IMapper _mapper;

        public GetAllActiveCompensationsByEmployeIdQueryHandler(ICompensationRepository compensationRepository, IMapper mapper)
        {
            _compensationRepository = compensationRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<GetCompensationDto>> Handle(GetAllActiveCompensationsByEmployeIdQuery request, CancellationToken cancellationToken)
        {
            var (compensations, totalCount) = await _compensationRepository.GetAllActiveForEmployeeIdAsync(request.EmployeeId, request.QueryOptions);
            var compensationsDto = _mapper.Map<List<GetCompensationDto>>(compensations);

            var result = new PagedResult<GetCompensationDto>(compensationsDto, totalCount, request.QueryOptions.PageSize, request.QueryOptions.PageNumber);
            return result;
        }
    }
}