using OvertimeManager.Domain.Entities.User;

namespace OvertimeManager.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<IEnumerable<Employee>> GetAllByManagerIdAsync(int id);
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee?> GetByEmailAsync(string email);
        Task<int> CreateEmployeeAsync(Employee employee);
        Task<string> GetEmployeeJwt(int id);
        Task SaveChangesAsync();
        Task DeleteAsync(Employee employee);
    }
}
