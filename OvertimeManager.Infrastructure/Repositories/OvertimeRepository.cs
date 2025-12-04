using Microsoft.EntityFrameworkCore;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Interfaces;
using OvertimeManager.Infrastructure.Persistence;

namespace OvertimeManager.Infrastructure.Repositories
{
    public class OvertimeRepository : IOvertimeRepository
    {
        private readonly OvertimeManagerDbContext _dbContext;

        public OvertimeRepository(OvertimeManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateOvertimeAsync(OvertimeRequest overtime)
        {
            await _dbContext.OvertimeRequests.AddAsync(overtime);
            await _dbContext.SaveChangesAsync();
            return overtime.Id;
        }

        public async Task<(IEnumerable<OvertimeRequest>, int)> GetAllForEmployeeIdAsync(int id, object queryOptions)
        {
            var baseQuery = _dbContext.OvertimeRequests
                .Include(o => o.RequestedForEmployee)
                .Include(o => o.RequestedByEmployee)
                .Include(o => o.ApprovedByEmployee)
                .Where(o => o.RequestedForEmployeeId == id);

            var apliedQuery = new FromQueryOptionsHandler<OvertimeRequest>((FromQueryOptions)queryOptions)
                .GetAppliedQueryWithTotalItemsCount(baseQuery);

            var itemsList = await apliedQuery.Item1.ToListAsync();
            return (itemsList, apliedQuery.Item2);
        }


        public async Task<(IEnumerable<OvertimeRequest>, int)> GetAllActiveForEmployeeIdAsync(int id, object queryOptions)
        {
            var baseQuery = _dbContext.OvertimeRequests
                .Include(o => o.RequestedForEmployee)
                .Include(o => o.RequestedByEmployee)
                .Include(o => o.ApprovedByEmployee)
                .Where(o => o.RequestedForEmployeeId == id)
                .Where(o => o.Status == "Pending" || o.Status == "Approved");

            var apliedQuery = new FromQueryOptionsHandler<OvertimeRequest>((FromQueryOptions)queryOptions)
                .GetAppliedQueryWithTotalItemsCount(baseQuery);

            var itemsList = await apliedQuery.Item1.ToListAsync();
            return (itemsList, apliedQuery.Item2);
        }

        public async Task<OvertimeRequest?> GetByIdAsync(int id)
            => await _dbContext.OvertimeRequests
                .Include(o => o.RequestedForEmployee)
                .Include(o => o.RequestedByEmployee)
                .Include(o => o.ApprovedByEmployee)
                .FirstOrDefaultAsync(o => o.Id == id);

        public async Task SaveChangesAsync() 
            => await _dbContext.SaveChangesAsync();

        public async Task<(IEnumerable<OvertimeRequest>, int)> GetAllForEmployeesByManagerId(int id, object queryOptions)
        {
            var baseQuery = _dbContext.OvertimeRequests
                .Include(o => o.RequestedForEmployee)
                .Include(o => o.RequestedByEmployee)
                .Include(o => o.ApprovedByEmployee)
                .Where(o => o.RequestedForEmployee!.ManagerId == id);
                

            var apliedQuery = new FromQueryOptionsHandler<OvertimeRequest>((FromQueryOptions)queryOptions)
                .GetAppliedQueryWithTotalItemsCount(baseQuery);

            var itemsList = await apliedQuery.Item1.ToListAsync();
            return (itemsList, apliedQuery.Item2);
        }

        public async Task<(IEnumerable<OvertimeRequest>, int)> GetAllActiveForEmployeesByManagerId(int id, object queryOptions)
        {
            var baseQuery = _dbContext.OvertimeRequests
                .Include(o => o.RequestedForEmployee)
                .Include(o => o.RequestedByEmployee)
                .Include(o => o.ApprovedByEmployee)
                .Where(o => o.RequestedForEmployee!.ManagerId == id)
                .Where(o => o.Status == "Pending" || o.Status == "Approved");

            var apliedQuery = new FromQueryOptionsHandler<OvertimeRequest>((FromQueryOptions)queryOptions)
                .GetAppliedQueryWithTotalItemsCount(baseQuery);

            var itemsList = await apliedQuery.Item1.ToListAsync();
            return (itemsList, apliedQuery.Item2);
        }
    }
}
