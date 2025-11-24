using MediatR;
using OvertimeManager.Domain.Constants;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Commands.CancelOvertime
{
    public class CancelOvertimeCommandHandler : IRequestHandler<CancelOvertimeCommand>
    {
        private readonly IOvertimeRepository _overtimeRepository;

        public CancelOvertimeCommandHandler(IOvertimeRepository overtimeRepository)
        {
            _overtimeRepository = overtimeRepository;
        }
        public async Task Handle(CancelOvertimeCommand request, CancellationToken cancellationToken)
        {
            var overtime = await _overtimeRepository.GetByIdAsync(request.OvertimeId);
            if (overtime == null)
                throw new NotFoundException("Overtime request not found.", request.OvertimeId.ToString());

            var overtimeEmployeeId = overtime.RequestedForEmployeeId;
            if (overtimeEmployeeId != request.CurrentEmployeeId)
                throw new UnauthorizedException("You are not authorized to update this overtime request.");

            overtime.Status = ((StatusEnum)StatusEnum.Cancelled).ToString();
            await _overtimeRepository.SaveChangesAsync();

        }
    }
}
