using OvertimeManager.Domain.Entities.User;

namespace OvertimeManager.Domain.Entities.Overtime
{
    public abstract class OvertimeRequestBase
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateOnly CreatedForDay { get; set; }

        public int RequestedByEmployeeId { get; set; }
        public virtual Employee? RequestedByEmployee { get; set; }

        public int RequestedForEmployeeId { get; set; }
        public virtual Employee? RequestedForEmployee { get; set; }




    }
}