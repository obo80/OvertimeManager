using MediatR;
using OvertimeManager.Application.CQRS.HR.Employees.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.HR.Employees.Queries.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<IEnumerable<EmployeeWithOvetimeDataDto>>
    {

    }
}
