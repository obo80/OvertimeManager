using MediatR;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.OvertimeCompensationRequest.Commands.DeleteOvertimeCompensationRequest
{
    public class DeleteOvertimeCompensationRequestCommandHandler : IRequestHandler<DeleteOvertimeCompensationRequestCommand>
    {
        private readonly IOvertimeCompensationRequestRepository _overtimeCompensationRequestRepository;

        public DeleteOvertimeCompensationRequestCommandHandler(IOvertimeCompensationRequestRepository overtimeCompensationRequestRepository)
        {
            _overtimeCompensationRequestRepository = overtimeCompensationRequestRepository;
        }
        public async Task Handle(DeleteOvertimeCompensationRequestCommand request, CancellationToken cancellationToken)
        {
            var comensationRequest = await _overtimeCompensationRequestRepository.GetAsyncById(request.Id);
            if (comensationRequest is null)
                throw new NotFoundException(nameof(Domain.Entities.Overtime.OvertimeCompensationRequest), request.Id.ToString());

            await _overtimeCompensationRequestRepository.Delete(comensationRequest);
        }
    }
}
