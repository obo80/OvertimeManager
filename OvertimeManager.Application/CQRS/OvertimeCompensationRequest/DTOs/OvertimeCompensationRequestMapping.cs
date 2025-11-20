using AutoMapper;
using OvertimeManager.Application.CQRS.OvertimeCompensationRequest.Commands.CreateOvertimeCompensationRequest;
using OvertimeManager.Application.CQRS.OvertimeCompensationRequest.DTOs;

namespace OvertimeManager.Application.CQRS.OvertimeRequest.DTOs
{
    public class OvertimeCompensationRequestMapping : Profile
    {
        public OvertimeCompensationRequestMapping()
        {
            CreateMap<Domain.Entities.Overtime.OvertimeCompensationRequest, OvertimeCompensationRequestDto>();

            CreateMap<CreateOvertimeCompensationRequestCommand, Domain.Entities.Overtime.OvertimeCompensationRequest>();
        }
    }
}
