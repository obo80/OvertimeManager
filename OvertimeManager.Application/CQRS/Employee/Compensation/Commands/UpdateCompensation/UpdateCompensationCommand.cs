using MediatR;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Commands.UpdateCompensation
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
    
}