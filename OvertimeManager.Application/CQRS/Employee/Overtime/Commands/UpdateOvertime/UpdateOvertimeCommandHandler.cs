using MediatR;
using OvertimeManager.Domain.Constants;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Commands.UpdateOvertime
{
    class UpdateOvertimeCommandHandler : IRequestHandler<UpdateOvertimeCommand>
    {
        private readonly IOvertimeRepository _overtimeRepository;
        

        public UpdateOvertimeCommandHandler(IOvertimeRepository overtimeRepository)
        {
            _overtimeRepository = overtimeRepository;
        }

        public async Task Handle(UpdateOvertimeCommand request, CancellationToken cancellationToken)
        {
            var overtime = await _overtimeRepository.GetByIdAsync(request.OvertimeId);
            if (overtime == null)
                throw new NotFoundException("Overtime request not found.", request.OvertimeId.ToString());

            var overtimeEmployeeId = overtime.RequestedForEmployeeId;
            if (overtimeEmployeeId != request.CurrentEmployeeId)
                throw new UnauthorizedException("You are not authorized to update this overtime request.");

            if (overtime.Status != ((StatusEnum)StatusEnum.Pending).ToString())
                throw new Domain.Exceptions.InvalidOperationException("Only pending overtime requests can be updated.");

            //if condition added to update values only with provided data and left other as they are
            if (request.Name != null)
                overtime.Name = request.Name;

            if (request.BusinessJustificationReason != null)
                overtime.BusinessJustificationReason = request.BusinessJustificationReason;

            if (request.RequestedTime != null)
                overtime.RequestedTime = (double)request.RequestedTime;


            await _overtimeRepository.SaveChangesAsync();

        }
    }



}
