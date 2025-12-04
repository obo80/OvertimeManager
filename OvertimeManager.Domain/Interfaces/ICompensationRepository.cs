using OvertimeManager.Domain.Entities.Overtime;

namespace OvertimeManager.Domain.Interfaces
{
    public interface ICompensationRepository
    {
        Task<(IEnumerable<CompensationRequest>, int)> GetAllForEmployeeIdAsync(int id, object queryOptions);
        Task<(IEnumerable<CompensationRequest>, int)> GetAllActiveForEmployeeIdAsync(int id, object queryOptions);
        Task<(IEnumerable<CompensationRequest>, int)> GetAllForEmployeesByManagerId(int id, object queryOptions);
        Task<(IEnumerable<CompensationRequest>, int)> GetAllActiveForEmployeesByManagerId(int id, object queryOptions);
        Task<CompensationRequest?> GetByIdAsync(int id);
        Task<int> CreateCompensationAsync(CompensationRequest compensation);
        Task SaveChangesAsync();
    }
}
