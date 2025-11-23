using AutoMapper;
using OvertimeManager.Application.CQRS.HR.Employees.Commands.CreateEmployee;

namespace OvertimeManager.Application.CQRS.HR.Employees.DTOs
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Domain.Entities.User.Employee, EmployeeWithOvetimeDataDto>()
                .ForMember(dto => dto.TakenOvertime,  c => c.MapFrom(e => e.OvertimeSummary.TakenOvertime))
                .ForMember(dto => dto.SettledOvertimet,  c => c.MapFrom(e => e.OvertimeSummary.SettledOvertimet))
                .ForMember(dto => dto.UnsetledOvertime,  c => c.MapFrom(e => e.OvertimeSummary.UnsetledOvertime));

            CreateMap<CreateEmployeeCommand, Domain.Entities.User.Employee>();
            //CreateMap<UpdateEmployeeCommand, Domain.Entities.User.Employee>();

            


        }
    }
}
