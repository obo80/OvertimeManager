using AutoMapper;
using MediatR;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.HR.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Domain.Entities.User.Employee>(request);
            employee.OvertimeSummary = new Domain.Entities.User.EmployeeOvertimeSummary();
            employee.MustChangePassword = true;

            var id = await _employeeRepository.CreateEmployeeAsync(employee);
            return id;
        }
    }
}
