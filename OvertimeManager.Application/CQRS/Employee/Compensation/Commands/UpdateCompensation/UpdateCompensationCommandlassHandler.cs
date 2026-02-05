using MediatR;
using OvertimeManager.Domain.Constants;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Commands.UpdateCompensation
{
    public class UpdateCompensationCommandlassHandler : IRequestHandler<UpdateCompensationCommand>
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateCompensationCommandlassHandler(ICompensationRepository compensationRepository, IEmployeeRepository employeeRepository)
        {
            _compensationRepository = compensationRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(UpdateCompensationCommand request, CancellationToken cancellationToken)
        {
            var compensation = await _compensationRepository.GetByIdAsync(request.CompensationId);
            if (compensation == null)
                throw new NotFoundException("Compensation request", request.CompensationId.ToString());

            var compensationEmployeeId = compensation.RequestedForEmployeeId;
            if (compensationEmployeeId != request.CurrentEmployeeId)
                throw new ForbidException("You are not authorized to update this compensation request.");

            if (compensation.Status != StatusEnum.Pending.ToString())
                throw new BadRequestException("Only pending compensation requests can be updated.");

            var employee = await _employeeRepository.GetByIdAsync(request.CurrentEmployeeId);
            if (employee == null)
                throw new NotFoundException("Employee", request.CurrentEmployeeId.ToString());



            //if condition added to update values only with provided data and left other as they are
            if (request.RequestedTime != null)
            {
                var canSettle = employee.OvertimeSummary!.CanSettleOvertime(request.RequestedTime.Value);
                if (!canSettle)
                    throw new BadRequestException("Insufficient unsettled overtime to settle the requested time.");

                compensation.RequestedTime = (double)request.RequestedTime;
                compensation.SetCompensation(request.IsMultiplied);
            }

            await _compensationRepository.SaveChangesAsync();
        }
    }
    
}