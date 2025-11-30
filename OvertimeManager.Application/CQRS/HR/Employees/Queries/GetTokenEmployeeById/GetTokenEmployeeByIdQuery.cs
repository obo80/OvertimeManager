using MediatR;

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
