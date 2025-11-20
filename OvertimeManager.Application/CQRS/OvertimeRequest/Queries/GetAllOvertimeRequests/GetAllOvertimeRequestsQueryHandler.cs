using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.OvertimeRequest.DTOs;
using OvertimeManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.OvertimeRequest.Queries.GetAllOvertimeRequests
{
    public class GetAllOvertimeRequestsQueryHandler : IRequestHandler<GetAllOvertimeRequestsQuery, IEnumerable<OvertimeRequestDto>>
    {
        private readonly IOvertimeRequestRepository _overtimeRequestRepository;
        private readonly IMapper _mapper;

        public GetAllOvertimeRequestsQueryHandler(IOvertimeRequestRepository overtimeRequestRepository, IMapper mapper)
        {
            _overtimeRequestRepository = overtimeRequestRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OvertimeRequestDto>> Handle(GetAllOvertimeRequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = await _overtimeRequestRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<OvertimeRequestDto>>(requests);

            return dtos;
        }
    }
}
