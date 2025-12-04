using OvertimeManager.Domain.Entities.Overtime;

namespace OvertimeManager.Domain.Interfaces
{
    public interface IOvertimeRepository
    {
        Task<(IEnumerable<OvertimeRequest>, int)> GetAllForEmployeeIdAsync(int id, object queryOptions);
        Task<(IEnumerable<OvertimeRequest>, int)> GetAllActiveForEmployeeIdAsync(int id, object queryOptions);
        Task<(IEnumerable<OvertimeRequest>, int)> GetAllForEmployeesByManagerId(int id, object queryOptions);
        Task<(IEnumerable<OvertimeRequest>, int)> GetAllActiveForEmployeesByManagerId(int id, object queryOptions);
        Task<OvertimeRequest?> GetByIdAsync(int id);
        Task<int> CreateOvertimeAsync(OvertimeRequest overtime);
        Task SaveChangesAsync();

    }
}
