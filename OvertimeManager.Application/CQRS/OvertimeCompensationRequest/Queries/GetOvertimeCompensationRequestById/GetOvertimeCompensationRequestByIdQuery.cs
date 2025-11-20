using MediatR;
using OvertimeManager.Application.CQRS.OvertimeCompensationRequest.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.OvertimeCompensationRequest.Queries.GetOvertimeCompensationRequestById
{
    public class GetOvertimeCompensationRequestByIdQuery :IRequest<OvertimeCompensationRequestDto>
    {
        public int Id { get; }
        public GetOvertimeCompensationRequestByIdQuery(int id)
        {
            Id = id;
        }

        
    }
}
