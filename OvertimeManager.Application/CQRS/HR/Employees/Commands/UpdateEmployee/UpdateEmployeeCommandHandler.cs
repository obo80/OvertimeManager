using MediatR;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.HR.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);
            if (employee is null)
                throw new NotFoundException(nameof(Employee), request.Id.ToString());


            //if condition added to update values only with provided data and left other as they are
            if (request.FirstName != null)
                employee.FirstName = request.FirstName;

            if (request.LastName != null)
                employee.LastName = request.LastName;

            if (request.Email != null)
                employee.Email = request.Email;

            if (request.RoleId != null)
                employee.RoleId = (int) request.RoleId;

            if (request.ManagerId != null)
                employee.ManagerId = request.ManagerId;

            await _employeeRepository.SaveChangesAsync();
        }
    }
}
