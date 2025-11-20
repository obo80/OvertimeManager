using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.OvertimeCompensationRequest.DTOs;
using OvertimeManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.OvertimeCompensationRequest.Queries.GetOvertimeCompensationRequestById
{
    internal class GetOvertimeCompensationRequestByIdQueryHandler : IRequestHandler<GetOvertimeCompensationRequestByIdQuery, OvertimeCompensationRequestDto>
    {
        private readonly IOvertimeCompensationRequestRepository _overtimeCompensationRequestRepository;
        private readonly IMapper _mapper;

        public GetOvertimeCompensationRequestByIdQueryHandler(IOvertimeCompensationRequestRepository overtimeCompensationRequestRepository, IMapper mapper)
        {
            _overtimeCompensationRequestRepository = overtimeCompensationRequestRepository;
            _mapper = mapper;
        }
        public async Task<OvertimeCompensationRequestDto> Handle(GetOvertimeCompensationRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var overtimeRequest = await _overtimeCompensationRequestRepository.GetAsyncById(request.Id);
            var dto = _mapper.Map<OvertimeCompensationRequestDto>(overtimeRequest);

            return dto;

        }
    }
}
