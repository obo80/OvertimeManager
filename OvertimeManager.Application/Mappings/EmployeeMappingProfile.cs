using AutoMapper;
using OvertimeManager.Application.CQRS.HR.Employees.Commands.CreateEmployee;
using OvertimeManager.Application.CQRS.HR.Employees.Commands.UpdateEmployee;
using OvertimeManager.Application.CQRS.HR.Employees.DTOs;

namespace OvertimeManager.Application.Mappings
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Domain.Entities.User.Employee, HREmployeeWithOvetimeDataDto>()
                .ForMember(dto => dto.TakenOvertime,  c => c.MapFrom(e => e.OvertimeSummary.TakenOvertime))
                .ForMember(dto => dto.SettledOvertime,  c => c.MapFrom(e => e.OvertimeSummary.SettledOvertime))
                .ForMember(dto => dto.UnsettledOvertime,  c => c.MapFrom(e => e.OvertimeSummary.UnsettledOvertime));

            CreateMap<CreateEmployeeCommand, Domain.Entities.User.Employee>();
            //CreateMap<UpdateEmployeeCommand, Domain.Entities.User.Employee>();

            CreateMap<CreateEmployeeDto, CreateEmployeeCommand>();
            CreateMap<UpdateEmployeeDto, UpdateEmployeeCommand>();







        }
    }
}
