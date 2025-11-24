using Microsoft.EntityFrameworkCore;
using OvertimeManager.Application.Common;
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
    public class OvertimeRepository : IOvertimeRepository
    {
        private readonly OvertimeManagerDbContext _dbContext;
        private readonly IJwtService _jwtService;

        public OvertimeRepository(OvertimeManagerDbContext dbContext, IJwtService jwtService)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
        }

        public async Task<int> CreateOvertimeAsync(OvertimeRequest overtime)
        {
            await _dbContext.AddAsync(overtime);
            await _dbContext.SaveChangesAsync();
            return overtime.Id;
        }

        public async Task DeleteAsync(OvertimeRequest overtime)
        {
            _dbContext.OvertimeRequests.Remove(overtime);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<OvertimeRequest>> GetAllAsync()
            => await _dbContext.OvertimeRequests
                .Include(o => o.RequestedForEmployee)
                .ToListAsync();

        public async Task<IEnumerable<OvertimeRequest>> GetAllByManagerIdAsync(int id)
            => await _dbContext.OvertimeRequests
                .Where(o => o.RequestedForEmployee.ManagerId == id)
                .Include(o => o.RequestedForEmployee)
                .ToListAsync();


        public async Task<IEnumerable<OvertimeRequest>> GetAllForEmployeeIdAsync(int id)
            => await _dbContext.OvertimeRequests
                .Include(o => o.RequestedForEmployee)
                .Include(o => o.RequestedByEmployee)
                .Include(o => o.ApprovedByEmployee)
                .Where(o => o.RequestedForEmployeeId == id)
                .ToListAsync();

        public async Task<OvertimeRequest?> GetByIdAsync(int id)
            => await _dbContext.OvertimeRequests
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
