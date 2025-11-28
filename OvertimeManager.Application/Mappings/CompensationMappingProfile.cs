using AutoMapper;
using OvertimeManager.Api.Controllers;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Application.CQRS.Manager.Compensation.Commands.CreateCompensationByManager;
using OvertimeManager.Application.CQRS.Manager.Compensation.Commands.UpdateCompensationByManager;
using OvertimeManager.Application.CQRS.Manager.Compensation.DTOs;
using OvertimeManager.Domain.Entities.Overtime;

namespace OvertimeManager.Application.Mappings
{
    public class CompensationMappingProfile :Profile
    {
        public CompensationMappingProfile()
        {
            CreateMap<CreateCompensationDto, CreateCompensationCommand>();
            CreateMap<CreateCompensationCommand, CompensationRequest>();

            CreateMap<CreateCompensationByManagerDto, CreateCompensationByManagerCommand>();
            CreateMap<CreateCompensationByManagerCommand, CompensationRequest>();

            CreateMap<UpdateCompensationDto, UpdateCompensationCommand>();
            CreateMap<UpdateCompensationByManagerDto, UpdateCompensationByManagerCommand>();


            CreateMap<SetCompensationDoneDto, SetCompensationDoneCommand>();

            CreateMap<CompensationRequest, GetCompensationDto>();
            CreateMap<GetCompensationDto, CompensationRequest>();
        }
    }
}
