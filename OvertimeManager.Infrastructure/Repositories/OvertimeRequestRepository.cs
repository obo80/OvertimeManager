using Microsoft.EntityFrameworkCore;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Entities.User;
using OvertimeManager.Domain.Interfaces;
using OvertimeManager.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Infrastructure.Repositories
{
    public class OvertimeRequestRepository : IOvertimeRequestRepository
    {
        private readonly OvertimeManagerDbContext _dbContext;

        public OvertimeRequestRepository(OvertimeManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Create(OvertimeRequest overtimeRequest)
        {
            await _dbContext.AddAsync(overtimeRequest);
            await _dbContext.SaveChangesAsync();
            return overtimeRequest.Id; ;
        }

        public async Task<IEnumerable<OvertimeRequest>> GetAllAsync()
            => await _dbContext.OvertimeRequests.ToListAsync();

        public async Task<OvertimeRequest?> GetAsyncById(int id)
            => await _dbContext.OvertimeRequests.FirstOrDefaultAsync(r => r.Id == id);

        public async Task Delete(OvertimeRequest overtimeRequest)
        {
            _dbContext.OvertimeRequests.Remove(overtimeRequest);
            await _dbContext.SaveChangesAsync();
        }
    }
}
