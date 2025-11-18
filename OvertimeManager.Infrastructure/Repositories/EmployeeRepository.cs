using Microsoft.EntityFrameworkCore;
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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly OvertimeManagerDbContext _dbContext;

        public EmployeeRepository(OvertimeManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Create(Employee employee)
        {
            await _dbContext.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
            => await _dbContext.Employees.ToListAsync();

        public async Task<Employee?> GetAsyncById(int id)
            =>await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
    }
}
