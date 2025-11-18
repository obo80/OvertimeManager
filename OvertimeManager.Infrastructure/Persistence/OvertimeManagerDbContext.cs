using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Entities.User;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Infrastructure.Persistence
{
    public class OvertimeManagerDbContext : DbContext
    {
        public OvertimeManagerDbContext(DbContextOptions<OvertimeManagerDbContext> options) : base(options) { }


        public DbSet<OvertimeRequest> OvertimeRequests { get; set; }
        public DbSet<OvertimeCompensationRequest> OvertimeCompensationRequests { get; set; }

        public DbSet<OvertimeRequestStatus> OvertimeRequestsStatusses { get; set; }
        public DbSet<OvertimeSummary> OvertimeSummaries { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OvertimeManagerDb;Trusted_Connection=True;");
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OvertimeRequest>(eb =>
            {
                eb.Property(r => r.Name).IsRequired();
                eb.Property(r => r.BusinessJustificationReason).IsRequired();
                eb.Property(r => r.RequesterdByEmployeeId).IsRequired();
                eb.Property(r => r.RequesedForEmployeeId).IsRequired();
                eb.Property(r => r.CreatedForDay).IsRequired();
                eb.Property(r => r.RequestedTime).IsRequired();
                eb.Property(r => r.ApprovalStatusId).IsRequired(); 
            });

            modelBuilder.Entity<OvertimeCompensationRequest>(eb =>
            {
                eb.Property(r => r.RequesterdByEmployeeId).IsRequired();
                eb.Property(r => r.RequesedForEmployeeId).IsRequired();
                eb.Property(r => r.CreatedForDay).IsRequired();
                eb.Property(r => r.RequestedTime).IsRequired();
                eb.Property(r => r.RequestedTime).IsRequired();
                eb.Property(r => r.Multiplier).IsRequired();
            });

            modelBuilder.Entity<OvertimeRequestStatus>(eb =>
            {
                eb.Property(s => s.Status).IsRequired();
            });

            modelBuilder.Entity<Employee>(eb => 
            {
                eb.Property(e => e.Email).IsRequired();
                eb.Property(e => e.FirstName).IsRequired();
                eb.Property(e => e.LastName).IsRequired();
                eb.Property(e => e.RoleId).IsRequired();
                //eb.Property(e => e.ManagerId).IsRequired();

                eb.HasOne(e => e.Manager)
                    .WithMany(e => e.Subordinates)
                    .HasForeignKey(e => e.ManagerId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
