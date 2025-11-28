using MediatR;
using OvertimeManager.Application.Common;
using OvertimeManager.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Commands.CreateOvertime
{
    public class CreateOvertimeCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; } = default;
        public string? BusinessJustificationReason { get; set; } = default;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateOnly CreatedForDay { get; set; } = DateOnly.FromDateTime(DateTime.Now); //default to today
        public int RequestedByEmployeeId { get; set; }
        public int RequestedForEmployeeId { get; set; }

        public double RequestedTime { get; set; }


        //public int ApprovalStatusId { get; set; } = 1;      //default to "Pending"
        public string Status { get; set; } = ((StatusEnum)StatusEnum.Pending).ToString();
        //public DateTime? ApprovedAt { get; set; } = null;
        //public int? ApprovedByEmployeeId { get; set; } = null!;

        //public double? ActualTime { get; set; } = null;

        //public string Authorization { get; }
        //public int CurrentEmployeeId { get; }

        public CreateOvertimeCommand(int currentEmployeeId)
        {
            RequestedByEmployeeId = currentEmployeeId;
            RequestedForEmployeeId = currentEmployeeId;
        }
    }
}
