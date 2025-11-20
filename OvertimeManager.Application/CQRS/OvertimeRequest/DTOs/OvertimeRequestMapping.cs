using AutoMapper;
using OvertimeManager.Application.CQRS.OvertimeRequest.Commands.CreateOvertimeRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.OvertimeRequest.DTOs
{
    public class OvertimeRequestMapping : Profile
    {
        public OvertimeRequestMapping()
        {
            CreateMap<Domain.Entities.Overtime.OvertimeRequest, OvertimeRequestDto>();

            CreateMap<CreateOvertimeRequestCommand, Domain.Entities.Overtime.OvertimeRequest>();
        }
    }
}
