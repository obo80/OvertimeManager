using MediatR;
using OvertimeManager.Domain.Constants;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Api.Controllers
{
    public class CancelCompensationCommandHandler : IRequestHandler<CancelCompensationCommand>
    {
        private readonly ICompensationRepository _compensationRepository;

        public CancelCompensationCommandHandler(ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
        }
        public async Task Handle(CancelCompensationCommand request, CancellationToken cancellationToken)
        {
            var compensation = await _compensationRepository.GetByIdAsync(request.CompensationId);
            if (compensation == null)
                throw new NotFoundException("Compensation request not found.", request.CompensationId.ToString());

            var compensationEmployeeId = compensation.RequestedForEmployeeId;
            if (compensationEmployeeId != request.CurrentEmployeeId)
                throw new UnauthorizedException("You are not authorized to update this compensation request.");

            compensation.Status = ((StatusEnum)StatusEnum.Cancelled).ToString();
            await _compensationRepository.SaveChangesAsync();
        }
    }
}