using Microsoft.EntityFrameworkCore;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Interfaces;
using OvertimeManager.Infrastructure.Persistence;

namespace OvertimeManager.Infrastructure.Repositories
{
    public class CompensationRepository(OvertimeManagerDbContext dbContext) : ICompensationRepository
    {
        private readonly OvertimeManagerDbContext _dbContext = dbContext;

        public async Task<int> CreateCompensationAsync(CompensationRequest newCompensation)
        {
            await _dbContext.OvertimeCompensationRequests.AddAsync(newCompensation);
            await _dbContext.SaveChangesAsync();
            return newCompensation.Id;
        }
        public async Task SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();



        public async Task<(IEnumerable<CompensationRequest>, int)> GetAllForEmployeesByManagerId(
            int id, object queryOptions)
        {
            var baseQuery = _dbContext.OvertimeCompensationRequests
                .Include(o => o.RequestedForEmployee)
                .Include(o => o.RequestedByEmployee)
                .Include(o => o.ApprovedByEmployee)
                .Where(o => o.RequestedForEmployee!.ManagerId == id);

            var apliedQuery = new FromQueryOptionsHandler<CompensationRequest>((FromQueryOptions)queryOptions)
                .GetAppliedQueryWithTotalItemsCount(baseQuery);

            var itemsList = await apliedQuery.Item1.ToListAsync();
            return (itemsList, apliedQuery.Item2);
        }

        public async Task<(IEnumerable<CompensationRequest>, int)> GetAllActiveForEmployeesByManagerId(
            int id, object queryOptions)
        {
            var baseQuery = _dbContext.OvertimeCompensationRequests
                .Include(o => o.RequestedForEmployee)
                .Include(o => o.RequestedByEmployee)
                .Include(o => o.ApprovedByEmployee)
                .Where(o => o.RequestedForEmployee!.ManagerId == id)
                .Where(o => o.Status == "Pending" || o.Status == "Approved");


            var apliedQuery = new FromQueryOptionsHandler<CompensationRequest>((FromQueryOptions)queryOptions)
                .GetAppliedQueryWithTotalItemsCount(baseQuery);

            var itemsList = await apliedQuery.Item1.ToListAsync();
            return (itemsList, apliedQuery.Item2);
        }

        public async Task<(IEnumerable<CompensationRequest>, int)> GetAllForEmployeeIdAsync(
            int id, object queryOptions)
        {
            var baseQuery = _dbContext.OvertimeCompensationRequests
                .Include(o => o.RequestedForEmployee)
                .Include(o => o.RequestedByEmployee)
                .Include(o => o.ApprovedByEmployee)
                .Where(o => o.RequestedForEmployeeId == id);

            var apliedQuery = new FromQueryOptionsHandler<CompensationRequest>((FromQueryOptions)queryOptions)
                .GetAppliedQueryWithTotalItemsCount(baseQuery);

            var itemsList = await apliedQuery.Item1.ToListAsync();
            return (itemsList, apliedQuery.Item2);
        }

        public async Task<(IEnumerable<CompensationRequest>, int)> GetAllActiveForEmployeeIdAsync(
            int id, object queryOptions)
        {
            var baseQuery = _dbContext.OvertimeCompensationRequests
                .Include(o => o.RequestedForEmployee)
                .Include(o => o.RequestedByEmployee)
                .Include(o => o.ApprovedByEmployee)
                .Where(o => o.RequestedForEmployeeId == id)
                .Where(o => o.Status == "Pending" || o.Status == "Approved");

            var apliedQuery = new FromQueryOptionsHandler<CompensationRequest>((FromQueryOptions)queryOptions)
                .GetAppliedQueryWithTotalItemsCount(baseQuery);

            var itemsList = await apliedQuery.Item1.ToListAsync();
            return (itemsList, apliedQuery.Item2);
        }


        public async Task<CompensationRequest?> GetByIdAsync(int id)
            => await _dbContext.OvertimeCompensationRequests
                .Include(o => o.RequestedForEmployee)
                .Include(o => o.RequestedByEmployee)
                .Include(o => o.ApprovedByEmployee)
                .FirstOrDefaultAsync(o => o.Id == id);

    }
}
