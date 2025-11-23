using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.HR.Employees.Queries.GetTokenEmployeeById
{
    public class GetTokenEmployeeByIdQuery: IRequest<string>
    {
        public int Id { get; }

        public GetTokenEmployeeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
