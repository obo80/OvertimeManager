using Microsoft.EntityFrameworkCore;
using OvertimeManager.Application.Common;
using OvertimeManager.Application.Common.GetFromQueryOptions;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Entities.User;
using OvertimeManager.Domain.Interfaces;
using OvertimeManager.Infrastructure.Persistence;

namespace OvertimeManager.Infrastructure.Repositories
{
    public class EmployeeRepository(OvertimeManagerDbContext dbContext, IJwtService jwtService) : IEmployeeRepository
    {
        private readonly OvertimeManagerDbContext _dbContext = dbContext;
        private readonly IJwtService _jwtService = jwtService;

        public async Task SaveChangesAsync() 
            => await _dbContext.SaveChangesAsync();

        public async Task<int> CreateEmployeeAsync(Employee employee)
        {
            await _dbContext.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return employee.Id;
        }
                  
        public async Task<(IEnumerable<Employee>, int)> GetAllAsync(object queryOptions)
        {
            var baseQuery = _dbContext.Employees
                .Include(e => e.OvertimeSummary);
            var apliedQuery = new FromQueryOptionsHandler<Employee>((FromQueryOptions)queryOptions)
                .GetAppliedQueryWithTotalItemsCount(baseQuery);

            var itemsList = await apliedQuery.Item1.ToListAsync();
            return (itemsList, apliedQuery.Item2);
        }

        public async Task<Employee?> GetByIdAsync(int id)
            => await _dbContext.Employees
                .Include(e => e.OvertimeSummary)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<Employee?> GetByEmailAsync(string email) 
            => await _dbContext.Employees
                .Include(e => e.OvertimeSummary)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Email == email);

        public async Task DeleteAsync(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Employee>, int)> GetAllByManagerIdAsync(int id, object queryOptions)
        {
            var baseQuery = _dbContext.Employees
                .Where(e => e.ManagerId == id)
                .Include(e => e.OvertimeSummary);

            var apliedQuery = new FromQueryOptionsHandler<Employee>((FromQueryOptions)queryOptions)
                .GetAppliedQueryWithTotalItemsCount(baseQuery);

            var itemsList = await apliedQuery.Item1.ToListAsync();
            return (itemsList, apliedQuery.Item2);
        }

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
