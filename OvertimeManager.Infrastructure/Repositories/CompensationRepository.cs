using Microsoft.EntityFrameworkCore;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Interfaces;
using OvertimeManager.Infrastructure.Persistence;

namespace OvertimeManager.Infrastructure.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly OvertimeManagerDbContext _dbContext;

        public CompensationRepository(OvertimeManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateCompensationAsync(CompensationRequest newCompensation)
        {
            await _dbContext.OvertimeCompensationRequests.AddAsync(newCompensation);
            await _dbContext.SaveChangesAsync();
            return newCompensation.Id;
        }

        public async Task DeleteAsync(CompensationRequest compensation)
        {
            _dbContext.OvertimeCompensationRequests.Remove(compensation);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<CompensationRequest>> GetAllAsync()
            => await _dbContext.OvertimeCompensationRequests
                .Include(o => o.RequestedForEmployee)
                .ToListAsync();
        

        public async Task<IEnumerable<CompensationRequest>> GetAllActiveAsync()
            => await _dbContext.OvertimeCompensationRequests
                .Include(o => o.RequestedForEmployee)
                .Where(o => o.Status == "Pending" || o.Status == "Approved")
                .ToListAsync();

        public async Task<IEnumerable<CompensationRequest>> GetAllForEmployeeIdAsync(int id)
            => await _dbContext.OvertimeCompensationRequests
                .Include(o => o.RequestedForEmployee)
                .Include(o => o.RequestedByEmployee)
                .Include(o => o.ApprovedByEmployee)
                .Where(o => o.RequestedForEmployeeId == id)
                .ToListAsync();

        public async Task<IEnumerable<CompensationRequest>> GetAllActiveForEmployeeIdAsync(int id)
            => await _dbContext.OvertimeCompensationRequests
                .Include(o => o.RequestedForEmployee)
                .Include(o => o.RequestedByEmployee)
                .Include(o => o.ApprovedByEmployee)
                .Where(o => o.RequestedForEmployeeId == id)
                .Where(o => o.Status == "Pending" || o.Status == "Approved")
                .ToListAsync();

        public async Task<CompensationRequest?> GetByIdAsync(int id)
            => await _dbContext.OvertimeCompensationRequests
                .Include(o => o.RequestedForEmployee)
                .Include(o => o.RequestedByEmployee)
                .Include(o => o.ApprovedByEmployee)
                .FirstOrDefaultAsync(o => o.Id == id);

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
