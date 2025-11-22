using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.OvertimeRequest.DTOs;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.OvertimeRequest.Queries.GetAllMyOvertimeRequests
{
    public class GetAllMyOvertimeRequestsQueryHandler : IRequestHandler<GetAllMyOvertimeRequestsQuery, IEnumerable<OvertimeRequestDto>>
    {
        private readonly IOvertimeRequestRepository _overtimeRequestRepository;
        private readonly IMapper _mapper;

        public GetAllMyOvertimeRequestsQueryHandler(IOvertimeRequestRepository overtimeRequestRepository, IMapper mapper)
        {
            _overtimeRequestRepository = overtimeRequestRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OvertimeRequestDto>> Handle(GetAllMyOvertimeRequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = await _overtimeRequestRepository.GetAllMyRequestsAsync(request.EmployeeId);
            var dtos = _mapper.Map<IEnumerable<OvertimeRequestDto>>(requests);
            return dtos;
        }
    }
}   
