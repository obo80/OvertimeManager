using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Commands.UpdateOvertime
{
    public class UpdateOvertimeCommand : IRequest
    {
        public string? Name { get; set; }
        public string? BusinessJustificationReason { get; set; }
        public double? RequestedTime { get; set; }
        public int CurrentEmployeeId { get; }
        public int OvertimeId { get; }
        public UpdateOvertimeCommand(int currentEmployeeId, int overtimeId)
        {
            CurrentEmployeeId = currentEmployeeId;
            OvertimeId = overtimeId;
        }


    }



}
