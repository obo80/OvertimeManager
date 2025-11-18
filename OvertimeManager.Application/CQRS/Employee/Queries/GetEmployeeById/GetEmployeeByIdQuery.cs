using MediatR;
using OvertimeManager.Application.CQRS.Employee.Queries.GetAllEmployees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.Employee.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDto>
    {
        public int Id { get; }
        public GetEmployeeByIdQuery(int id)
        {
            Id = id;
        }


    }
}
