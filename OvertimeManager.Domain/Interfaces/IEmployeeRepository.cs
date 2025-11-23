using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<IEnumerable<Employee>> GetAllByManagerIdAsync(int id);
        Task<Employee?> GetAsyncById(int id);
        Task<Employee?> GetByEmail(string email);
        Task<int> Create(Employee employee);
        Task<string> GetEmployeeJwt(int id);
        Task SaveChanges();
        Task Delete(Employee employee);
    }
}
