using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Commands.CancelOvertime
{
    public class CancelOvertimeCommand :IRequest
    {
        public int CurrentEmployeeId { get; }
        public int OvertimeId { get; }

        public CancelOvertimeCommand(int currentEmployeeId, int overtimeId)
        {
            CurrentEmployeeId = currentEmployeeId;
            OvertimeId = overtimeId;
        }


    }
}
