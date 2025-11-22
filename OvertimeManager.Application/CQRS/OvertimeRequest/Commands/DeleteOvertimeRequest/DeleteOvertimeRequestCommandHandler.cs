using MediatR;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;


namespace OvertimeManager.Application.CQRS.OvertimeRequest.Commands.DeleteOvertimeRequest
{
    public class DeleteOvertimeRequestCommandHandler : IRequestHandler<DeleteOvertimeRequestCommand>
    {
        private readonly IOvertimeRequestRepository _overtimeRequestRepository;

        public DeleteOvertimeRequestCommandHandler(IOvertimeRequestRepository overtimeRequestRepository)
        {
            _overtimeRequestRepository = overtimeRequestRepository;
        }

        public async Task Handle(DeleteOvertimeRequestCommand request, CancellationToken cancellationToken)
        {
            var overtimeRequest = await _overtimeRequestRepository.GetAsyncById(request.Id);
            if (overtimeRequest is null)
                throw new NotFoundException(nameof(Domain.Entities.Overtime.OvertimeRequest), request.Id.ToString());

            await _overtimeRequestRepository.Delete(overtimeRequest);
        }
    }
}
