using MediatR;
using OvertimeManager.Application.CQRS.OvertimeRequest.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.OvertimeRequest.Queries.GetAllOvertimeRequests
{
    public class GetAllOvertimeRequestsQuery : IRequest<IEnumerable<OvertimeRequestDto>>
    {
    }
}
