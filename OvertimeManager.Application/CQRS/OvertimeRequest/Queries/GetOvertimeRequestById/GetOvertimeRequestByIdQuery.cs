using MediatR;
using OvertimeManager.Application.CQRS.OvertimeRequest.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.OvertimeRequest.Queries.GetOvertimeRequestById
{
    public class GetOvertimeRequestByIdQuery : IRequest<OvertimeRequestDto>
    {
        public int Id { get; }
        public GetOvertimeRequestByIdQuery(int id)
        {
            Id = id;
        }
    }
}
