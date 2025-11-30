using AutoMapper;
using OvertimeManager.Api.Controllers;
using OvertimeManager.Application.CQRS.Employee.Overtime.Commands.CreateOvertime;
using OvertimeManager.Application.CQRS.Employee.Overtime.Commands.UpdateOvertime;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Entities.Overtime;

namespace OvertimeManager.Application.Mappings
{
    public class OvertimeMappingProfile :Profile
    {
        public OvertimeMappingProfile()
        {
            CreateMap<CreateOvertimeDto, CreateOvertimeCommand>();
            CreateMap<CreateOvertimeCommand, OvertimeRequest>();

            CreateMap<OvertimeRequest, GetOvertimeDto>();
            CreateMap<GetOvertimeDto, OvertimeRequest>();

            CreateMap<UpdateOvertimeDto, UpdateOvertimeCommand>();
            CreateMap<SetOvertimeDoneDto, SetOvertimeDoneCommand>();
        }
    }
}
