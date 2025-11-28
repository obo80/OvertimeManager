using OvertimeManager.Domain.Entities.Overtime;

namespace OvertimeManager.Domain.Interfaces
{
    public interface ICompensationRepository
    {
        Task<IEnumerable<CompensationRequest>> GetAllAsync();
        Task<IEnumerable<CompensationRequest>> GetAllActiveAsync();
        Task<IEnumerable<CompensationRequest>> GetAllForEmployeeIdAsync(int id);
        Task<IEnumerable<CompensationRequest>> GetAllActiveForEmployeeIdAsync(int id);
        Task<CompensationRequest?> GetByIdAsync(int id);
        Task<int> CreateCompensationAsync(CompensationRequest compensation);
        Task DeleteAsync(CompensationRequest compensation);

        Task SaveChangesAsync();
    }
}
