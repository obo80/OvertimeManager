using Microsoft.EntityFrameworkCore;
using OvertimeManager.Application.Common;
using OvertimeManager.Domain.Entities.User;
using OvertimeManager.Domain.Interfaces;
using OvertimeManager.Infrastructure.Authentication;
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
        private readonly IJwtService _jwtService;

        public EmployeeRepository(OvertimeManagerDbContext dbContext, IJwtService jwtService)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CreateEmployeeAsync(Employee employee)
        {
            await _dbContext.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return employee.Id;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
            => await _dbContext.Employees
                .Include(e => e.OvertimeSummary)
                .ToListAsync();

        public async Task<Employee?> GetByIdAsync(int id)
            => await _dbContext.Employees
                .Include(e => e.OvertimeSummary)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<Employee?> GetByEmailAsync(string email)
        {
            return await _dbContext.Employees
                .Include(e => e.OvertimeSummary)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task DeleteAsync(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllByManagerIdAsync(int id)
            => await _dbContext.Employees
            .Where(e => e.ManagerId == id)
            .Include(e => e.OvertimeSummary)
            .ToListAsync();

        public async Task<string> GetEmployeeJwt(int id)
        {
            var employee = await _dbContext.Employees
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (employee is null)
            {
                throw new KeyNotFoundException("Employee not found");
            }
            return _jwtService.GenerateJwtToken(employee);
        }


    }
}
