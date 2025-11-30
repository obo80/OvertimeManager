namespace OvertimeManager.Application.CQRS.Manager.Compensation.DTOs
{
    public class CreateCompensationByManagerDto
    {
        public int RequestedForEmployeeId { get; set; }
        public double RequestedTime { get; set; }
    }
}
