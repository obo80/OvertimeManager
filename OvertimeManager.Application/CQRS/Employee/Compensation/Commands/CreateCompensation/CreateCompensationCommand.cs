using MediatR;
using OvertimeManager.Domain.Constants;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Commands.CreateCompensation
{
    public class CreateCompensationCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateOnly CreatedForDay { get; set; } = DateOnly.FromDateTime(DateTime.Now); //default - today
        public int RequestedByEmployeeId { get; set; }
        public int RequestedForEmployeeId { get; set; }

        public double RequestedTime { get; set; }

        public string Status { get; set; } = StatusEnum.Pending.ToString();

        public bool IsMultiplied { get; set; } = false; // Employee requests are not multiplied

        public CreateCompensationCommand(int currentEmployeeId)
        {
            RequestedByEmployeeId = currentEmployeeId;
            RequestedForEmployeeId = currentEmployeeId;
        }

    }
}