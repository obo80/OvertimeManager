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

namespace OvertimeManager.Application.CQRS.OvertimeRequest.Queries.GetOvertimeRequestById
{
    internal class GetOvertimeRequestByIdQueryHandler : IRequestHandler<GetOvertimeRequestByIdQuery, OvertimeRequestDto>
    {
        private readonly IOvertimeRequestRepository _overtimeRequestRepository;
        private readonly IMapper _mapper;

        public GetOvertimeRequestByIdQueryHandler(IOvertimeRequestRepository overtimeRequestRepository, IMapper mapper)
        {
            _overtimeRequestRepository = overtimeRequestRepository;
            _mapper = mapper;
        }
        public async Task<OvertimeRequestDto> Handle(GetOvertimeRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var overtimeRequest = await _overtimeRequestRepository.GetAsyncById(request.Id);
            var dto = _mapper.Map<OvertimeRequestDto>(overtimeRequest);

            return dto;
        }
    }
}
