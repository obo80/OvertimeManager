using AutoMapper;
using MediatR;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Queries.GetAllCompensationsByEmployeId
{
    public class GetAllCompensationsByEmployeIdQueryHandler : IRequestHandler<GetAllCompensationsByEmployeIdQuery, PagedResult<GetCompensationDto>>
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IMapper _mapper;

        public GetAllCompensationsByEmployeIdQueryHandler(ICompensationRepository compensationRepository, IMapper mapper)
        {
            _compensationRepository = compensationRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<GetCompensationDto>> Handle(GetAllCompensationsByEmployeIdQuery request, CancellationToken cancellationToken)
        {
            var (compensations, totalCount) = await _compensationRepository.GetAllForEmployeeIdAsync(request.EmployeeId, request.QueryOptions);
            var compensationsDto = _mapper.Map<IEnumerable<GetCompensationDto>>(compensations);

            var result = new PagedResult<GetCompensationDto>(compensationsDto, totalCount, request.QueryOptions.PageSize, request.QueryOptions.PageNumber);
            return result;
        }
    }


}