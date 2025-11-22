using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.OvertimeRequest.Commands.CreateOvertimeRequest;
using OvertimeManager.Domain.Constants;
using OvertimeManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.OvertimeCompensationRequest.Commands.CreateOvertimeCompensationRequest
{
    public class CreateOvertimeCompensationRequestCommandHandler : IRequestHandler<CreateOvertimeCompensationRequestCommand, int>
    {
        private readonly IOvertimeCompensationRequestRepository _overtimeCompensationRequestRepository;
        private readonly IMapper _mapper;

        public CreateOvertimeCompensationRequestCommandHandler(IOvertimeCompensationRequestRepository overtimeCompensationRequestRepository, IMapper mapper)
        {
            _overtimeCompensationRequestRepository = overtimeCompensationRequestRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateOvertimeCompensationRequestCommand request, CancellationToken cancellationToken)
        {
            var compensationRequest = _mapper.Map<Domain.Entities.Overtime.OvertimeCompensationRequest>(request);
            compensationRequest.CreatedAt = DateTime.UtcNow;
            var isMultiplied = CheckIfCompensationIsMultiplied(request.RequesterdByEmployeeId, request.RequesedForEmployeeId);
            compensationRequest.SetComensation(isMultiplied);

            var id = await _overtimeCompensationRequestRepository.Create(compensationRequest);

            return id;

        }



        private bool CheckIfCompensationIsMultiplied(int requesterdByEmployeeId, int requesedForEmployeeId)
        {
            if (requesterdByEmployeeId != requesedForEmployeeId) 
                return true;

            return false;
        }
    }
}
