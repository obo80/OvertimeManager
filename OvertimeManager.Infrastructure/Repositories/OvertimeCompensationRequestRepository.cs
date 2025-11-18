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
    public class OvertimeCompensationRequestRepository : IOvertimeCompensationRequestRepository
    {
        private readonly OvertimeManagerDbContext _dbContext;

        public OvertimeCompensationRequestRepository(OvertimeManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Create(OvertimeCompensationRequest overtimeRequest)
        {
            await _dbContext.AddAsync(overtimeRequest);
            await _dbContext.SaveChangesAsync();
        }

        //public async Task<IEnumerable<OvertimeCompensationRequest>> GetAllAsync()
        //    => await _dbContext.OvertimeCompensationRequest

        //public async Task<OvertimeCompensationRequest> GetAsyncById(int id)

    }
}
