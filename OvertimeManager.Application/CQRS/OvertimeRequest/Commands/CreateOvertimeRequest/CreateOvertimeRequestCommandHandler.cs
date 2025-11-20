using AutoMapper;
using MediatR;
using OvertimeManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.OvertimeRequest.Commands.CreateOvertimeRequest
{
    public class CreateOvertimeRequestCommandHandler : IRequestHandler<CreateOvertimeRequestCommand, int>
    {
        private readonly IOvertimeRequestRepository _overtimeRequestRepository;
        private readonly IMapper _mapper;

        public CreateOvertimeRequestCommandHandler(IOvertimeRequestRepository overtimeRequestRepository, IMapper mapper)
        {
            _overtimeRequestRepository = overtimeRequestRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateOvertimeRequestCommand request, CancellationToken cancellationToken)
        {
            var overtimeRequst = _mapper.Map<Domain.Entities.Overtime.OvertimeRequest>(request);
            overtimeRequst.ApprovalStatusId = 1;        //waiting
            overtimeRequst.CreatedAt = DateTime.UtcNow;

            var id = await _overtimeRequestRepository.Create(overtimeRequst);

            return id;
        }
    }


}
