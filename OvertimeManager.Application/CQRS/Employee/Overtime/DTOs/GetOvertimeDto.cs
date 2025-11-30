using OvertimeManager.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.DTOs
{
    public class GetOvertimeDto
    {
        public int Id { get; set; }

        //request details
        public string Name { get; set; } = default!;
        public string BusinessJustificationReason { get; set; } = default!;
        public double RequestedTime { get; set; }


        public DateTime CreatedAt { get; set; } 
        public DateOnly CreatedForDay { get; set; }

        public int RequestedByEmployeeId { get; set; }
        public string RequestedByEmployeeEmail { get; set; } = default!;

        public int RequestedForEmployeeId { get; set; }
        public string RequestedForEmployeeEmail { get; set; } = default!;


        //approval status is "Pending" by default on creation, approval process will update these fields
        public string Status { get; set; } = ((StatusEnum)StatusEnum.Pending).ToString();
        public DateTime? ApprovedAt { get; set; } = null;
        public int? ApprovedByEmployeeId { get; set; } = null;
        public string ApprovedByEmployeeEmail { get; set; }

        //actual time worked will be set after the overtime is completed
        public double? ActualTime { get; set; } = null;


    }
}
