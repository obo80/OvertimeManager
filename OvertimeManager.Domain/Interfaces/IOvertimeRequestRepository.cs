using OvertimeManager.Domain.Entities.Overtime;
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
        Task<OvertimeRequest> GetAsyncById(int id);
        Task Create(OvertimeRequest overtimeRequest);
        Task Commit();
    }
}
