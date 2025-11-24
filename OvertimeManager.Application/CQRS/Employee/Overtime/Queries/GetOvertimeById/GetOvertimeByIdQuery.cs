using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Queries.GetOvertimeById
{
    public class GetOvertimeByIdQuery : IRequest<GetOvertimeDto>
    {
        public int CurrentEmployeeId { get; }
        public int OvertimeId { get; }

        public GetOvertimeByIdQuery(int currentEmployeeId, int overtimeId)
        {
            CurrentEmployeeId = currentEmployeeId;
            OvertimeId = overtimeId;
        }
    }
}
