using MediatR;
using OvertimeManager.Application.Common;
using OvertimeManager.Application.CQRS.OvertimeRequest.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.OvertimeRequest.Queries.GetAllMyOvertimeRequests
{
    public class GetAllMyOvertimeRequestsQuery : IRequest<IEnumerable<OvertimeRequestDto>>
    {
        
        public int EmployeeId { get; set; }


        public GetAllMyOvertimeRequestsQuery(string authorizationToken)
        {
            EmployeeId = ApplicationUtils.GetUserIdFromClaims(authorizationToken);
        }
    }
}   
