using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.OvertimeCompensationRequest.Commands.DeleteOvertimeCompensationRequest
{
    public class DeleteOvertimeCompensationRequestCommand :IRequest
    {
        public int Id { get; }
        public DeleteOvertimeCompensationRequestCommand(int id)
        {
            Id = id;
        }

        
    }
}
