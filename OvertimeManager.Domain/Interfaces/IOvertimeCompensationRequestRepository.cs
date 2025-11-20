using OvertimeManager.Domain.Entities.Overtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Domain.Interfaces
{
    public  interface IOvertimeCompensationRequestRepository
    {
        Task<IEnumerable<OvertimeCompensationRequest>> GetAllAsync();
        Task<OvertimeCompensationRequest?> GetAsyncById(int id);
        Task<int> Create(OvertimeCompensationRequest overtimeCompensationRequest);
        Task SaveChanges();
        Task Delete(OvertimeCompensationRequest overtimeCompensationRequest);
    }
}
