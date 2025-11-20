using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.OvertimeCompensationRequest.DTOs;
using OvertimeManager.Application.CQRS.OvertimeRequest.DTOs;
using OvertimeManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.OvertimeCompensationRequest.Queries.GetAllOvertimeCompensationRequests
{

    public class GetAllOvertimeCompensationRequestsQueryHandler 
        : IRequestHandler<GetAllOvertimeCompensationRequestsQuery, IEnumerable<OvertimeCompensationRequestDto>>
    {
        private readonly IOvertimeCompensationRequestRepository _overtimeCompensationRequestRepository;
        private readonly IMapper _mapper;

        public GetAllOvertimeCompensationRequestsQueryHandler(IOvertimeCompensationRequestRepository overtimeCompensationRequestRepository, IMapper mapper)
        {
            _overtimeCompensationRequestRepository = overtimeCompensationRequestRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OvertimeCompensationRequestDto>> Handle(GetAllOvertimeCompensationRequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = await _overtimeCompensationRequestRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<OvertimeCompensationRequestDto>>(requests);

            return dtos;
        }
    }
}
