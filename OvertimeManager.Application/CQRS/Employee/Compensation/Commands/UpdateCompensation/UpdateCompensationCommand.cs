using MediatR;
using OvertimeManager.Domain.Constants;
using OvertimeManager.Domain.Entities.User;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;
using InvalidOperationException = OvertimeManager.Domain.Exceptions.InvalidOperationException;

namespace OvertimeManager.Api.Controllers
{
    public class UpdateCompensationCommand: IRequest
    {
        public double? RequestedTime { get; set; }
        public int CurrentEmployeeId { get; }
        public int CompensationId { get; }

        public bool IsMultiplied { get; set; } = false; // Employee requests are not multiplied
        public UpdateCompensationCommand(int currentEmployeeId, int id)
        {
            CurrentEmployeeId = currentEmployeeId;
            CompensationId = id;
        }

    }

    public class MyCUpdateCompensationCommandlassHandler : IRequestHandler<UpdateCompensationCommand>
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public MyCUpdateCompensationCommandlassHandler(ICompensationRepository compensationRepository, IEmployeeRepository employeeRepository)
        {
            _compensationRepository = compensationRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(UpdateCompensationCommand request, CancellationToken cancellationToken)
        {
            var compensation = await _compensationRepository.GetByIdAsync(request.CompensationId);
            if (compensation == null)
                throw new NotFoundException("Compensation request not found.", request.CompensationId.ToString());

            var compensationEmployeeId = compensation.RequestedForEmployeeId;
            if (compensationEmployeeId != request.CurrentEmployeeId)
                throw new UnauthorizedException("You are not authorized to update this compensation request.");

            if (compensation.Status != ((StatusEnum)StatusEnum.Pending).ToString())
                throw new InvalidOperationException("Only pending compensation requests can be updated.");

            var employee = await _employeeRepository.GetByIdAsync(request.CurrentEmployeeId);
            if (employee == null)
                throw new NotFoundException("Employee not found.", request.CurrentEmployeeId.ToString());



            //if condition added to update values only with provided data and left other as they are
            if (request.RequestedTime != null)
            {
                var canSettle = employee.OvertimeSummary.CanSettleOvertime(request.RequestedTime.Value);
                if (!canSettle)
                    throw new InvalidOperationException("Insufficient unsettled overtime to settle the requested time.");

                compensation.RequestedTime = (double)request.RequestedTime;
                compensation.SetCompensation(request.IsMultiplied);
            }

            await _compensationRepository.SaveChangesAsync();
        }
    }
    
}