using MediatR;
using OvertimeManager.Application.CQRS.CommonCQRS;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Commands.UpdateCompensationByManager
{
    public class UpdateCompensationByManagerCommandHandler : IRequestHandler<UpdateCompensationByManagerCommand>
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateCompensationByManagerCommandHandler(ICompensationRepository compensationRepository, IEmployeeRepository employeeRepository)
        {
            _compensationRepository = compensationRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(UpdateCompensationByManagerCommand request, CancellationToken cancellationToken)
        {
            if (request.RequestedForEmployeeId != null || request.RequestedTime != null) //anything to update provided
            {
                var compensation = await _compensationRepository.GetByIdAsync(request.CompensationId);
                if (compensation == null)
                    throw new NotFoundException("Compensation request not found.", request.CompensationId.ToString());

                if (request.RequestedForEmployeeId != null)
                {
                    if (await EmployeeHelper.IsEmployeeUnderManager(request.RequestedForEmployeeId.Value, request.CurrentManagerId, _employeeRepository))
                        throw new ForbidException($"You are not authorized to create compensation request for employee id={request.RequestedForEmployeeId}");

                    compensation.RequestedForEmployeeId = request.RequestedForEmployeeId.Value;
                }

                if (request.RequestedTime != null)
                {
                    compensation.RequestedTime = request.RequestedTime.Value;
                    compensation.SetCompensation(request.IsMultiplied);
                }

                var employee = await _employeeRepository.GetByIdAsync(compensation.RequestedForEmployeeId);
                if (employee == null)
                    throw new NotFoundException("Employee not found.", compensation.RequestedForEmployeeId.ToString());

                var canSettle = employee.OvertimeSummary.CanSettleOvertime(compensation.CompensatedTime);
                if (!canSettle)
                    throw new BadRequestException("Insufficient unsettled overtime to settle the requested time.");

                await _compensationRepository.SaveChangesAsync(); 
            }

        }
    }
}
