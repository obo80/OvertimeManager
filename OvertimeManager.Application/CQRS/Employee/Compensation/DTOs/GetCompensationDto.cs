using OvertimeManager.Domain.Constants;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.DTOs
{
    public class GetCompensationDto
    {
        public int Id { get; set; }

        //request details
        public double RequestedTime { get; set; }
        public double Multiplier { get; private set; }
        public double CompensatedTime { get; private set; }

        public DateTime CreatedAt { get; set; }
        public DateOnly CreatedForDay { get; set; }

        public int RequestedByEmployeeId { get; set; }
        public string RequestedByEmployeeEmail { get; set; } = default!;

        public int RequestedForEmployeeId { get; set; }
        public string RequestedForEmployeeEmail { get; set; } = default!;


        //approval status is "Pending" by default on creation by emplpoyee, bur manager automatically approves it during createion
        //approval process will update these fields
        public string Status { get; set; } = ((StatusEnum)StatusEnum.Pending).ToString();
        public DateTime? ApprovedAt { get; set; } = null;
        public int? ApprovedByEmployeeId { get; set; } = null;
        public string? ApprovedByEmployeeEmail { get; set; }
    }
}