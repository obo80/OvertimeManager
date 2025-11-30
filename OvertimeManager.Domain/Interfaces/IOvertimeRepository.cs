using OvertimeManager.Domain.Entities.Overtime;

namespace OvertimeManager.Domain.Interfaces
{
    public interface IOvertimeRepository
    {
        Task<IEnumerable<OvertimeRequest>> GetAllAsync();
        Task<IEnumerable<OvertimeRequest>> GetAllActiveAsync();
        Task<IEnumerable<OvertimeRequest>> GetAllForEmployeeIdAsync(int id);
        Task<IEnumerable<OvertimeRequest>> GetAllActiveForEmployeeIdAsync(int id);
        Task<OvertimeRequest?> GetByIdAsync(int id);
        Task<int> CreateOvertimeAsync(OvertimeRequest overtime);
        Task DeleteAsync(OvertimeRequest overtime);

        Task SaveChangesAsync();

    }
}
