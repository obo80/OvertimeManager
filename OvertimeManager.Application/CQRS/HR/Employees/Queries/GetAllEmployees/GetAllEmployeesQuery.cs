using MediatR;
using OvertimeManager.Application.CQRS.HR.Employees.DTOs;

namespace OvertimeManager.Application.CQRS.HR.Employees.Queries.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<IEnumerable<HREmployeeWithOvetimeDataDto>>
    {

    }
}
