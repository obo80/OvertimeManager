using OvertimeManager.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.Common
{
    public interface IJwtService
    {
        string GenerateJwtToken(Employee employee);
    }
}
