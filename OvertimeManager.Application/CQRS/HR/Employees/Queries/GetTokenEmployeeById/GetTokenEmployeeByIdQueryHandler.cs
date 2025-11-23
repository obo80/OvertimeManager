using MediatR;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.HR.Employees.Queries.GetTokenEmployeeById
{
    public class GetTokenEmployeeByIdQueryHandler : IRequestHandler<GetTokenEmployeeByIdQuery, string>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetTokenEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<string> Handle(GetTokenEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetEmployeeJwt(request.Id);
        }
    }
}
