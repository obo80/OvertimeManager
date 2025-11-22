using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Interfaces
{
    public  interface IOvertimeRequestRepository
    {
        Task<IEnumerable<OvertimeRequest>> GetAllAsync();
        Task<OvertimeRequest?> GetAsyncById(int id);
        Task<int> Create(OvertimeRequest overtimeRequest);
        Task SaveChanges();
        Task Delete(OvertimeRequest overtimeRequest);
        Task<IEnumerable<OvertimeRequest>> GetAllMyRequestsAsync(int employeeId);
        Task<IEnumerable<OvertimeRequest>> GetAllManagerSubordinatesRequestsAsync(int managerId);
    }
}
