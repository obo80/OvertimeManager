using MediatR;
using OvertimeManager.Application.CQRS.OvertimeCompensationRequest.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.OvertimeCompensationRequest.Queries.GetAllOvertimeCompensationRequests
{
    public class GetAllOvertimeCompensationRequestsQuery :IRequest<IEnumerable<OvertimeCompensationRequestDto>>
    {
    }
}
