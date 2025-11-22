using MediatR;
using OvertimeManager.Application.Common;
using OvertimeManager.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.OvertimeRequest.Commands.CreateOvertimeRequest
{
    public class CreateOvertimeRequestCommand : IRequest<int>
    {
                //public int Id { get; set; }
        public string Name { get; set; }
        public string BusinessJustificationReason { get; set; }
        //public DateTime CreatedAt { get; set; }
        public DateOnly CreatedForDay { get; set; }
        public double RequestedTime { get; set; }

        public int RequesterdByEmployeeId { get; set; }
        //public virtual Domain.Entities.User.Employee? RequestedByEmployee { get; set; }

        public int RequesedForEmployeeId { get; set; }
        //public virtual Domain.Entities.User.Employee? RequestedForEmployee { get; set; }

        //public double? ActualTime { get; set; }       
        //public int ApprovalStatusId { get; set; }

        public CreateOvertimeRequestCommand(string authorizationToken, bool isManagerRequest)
        {
            RequesterdByEmployeeId = ApplicationUtils.GetUserIdFromClaims(authorizationToken);

            if (!isManagerRequest)
                RequesedForEmployeeId = RequesterdByEmployeeId;

        }
    }
}
