using Microsoft.EntityFrameworkCore;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Entities.User;

namespace OvertimeManager.Infrastructure.Persistence
{
    public class OvertimeManagerDbContext : DbContext
    {
        public OvertimeManagerDbContext(DbContextOptions<OvertimeManagerDbContext> options) : base(options) { }


        public DbSet<OvertimeRequest> OvertimeRequests { get; set; }
        public DbSet<CompensationRequest> OvertimeCompensationRequests { get; set; }

        public DbSet<EmployeeOvertimeSummary> OvertimeSummaries { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OvertimeRequest>(eb =>
            {
                eb.Property(r => r.Name).IsRequired();
                eb.Property(r => r.BusinessJustificationReason).IsRequired();
                eb.Property(r => r.RequestedByEmployeeId).IsRequired();
                eb.Property(r => r.RequestedForEmployeeId).IsRequired();
                eb.Property(r => r.CreatedForDay).IsRequired();
                eb.Property(r => r.RequestedTime).IsRequired();

                eb.HasOne(r => r.RequestedByEmployee)
                    .WithMany()
                    .IsRequired(false)
                    .HasForeignKey(r => r.RequestedByEmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                eb.HasOne(r => r.RequestedForEmployee)
                   .WithMany()
                   .HasForeignKey(r => r.RequestedForEmployeeId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CompensationRequest>(eb =>
            {
                eb.Property(r => r.RequestedByEmployeeId).IsRequired();
                eb.Property(r => r.RequestedForEmployeeId).IsRequired();
                eb.Property(r => r.CreatedForDay).IsRequired();
                eb.Property(r => r.RequestedTime).IsRequired();
                eb.Property(r => r.RequestedTime).IsRequired();
                eb.Property(r => r.Multiplier).IsRequired();

                eb.HasOne(r => r.RequestedByEmployee)
                    .WithMany()
                    .IsRequired(false)
                    .HasForeignKey(r => r.RequestedByEmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                eb.HasOne(r => r.RequestedForEmployee)
                   .WithMany()
                   .HasForeignKey(r => r.RequestedForEmployeeId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<Employee>(eb => 
            {
                eb.Property(e => e.Email).IsRequired();
                eb.Property(e => e.FirstName).IsRequired();
                eb.Property(e => e.LastName).IsRequired();
                eb.Property(e => e.RoleId).IsRequired();


                eb.HasOne(e => e.Manager)
                    .WithMany(e => e.Subordinates)
                    .HasForeignKey(e => e.ManagerId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
