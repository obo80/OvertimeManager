using MediatR;
using OvertimeManager.Application.CQRS.Employee.Commands.CreateEmployee;
using OvertimeManager.Domain.Entities.User;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.Employee.Commands.UpdateEmployee
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
            var employee = await _employeeRepository.GetAsyncById(request.Id);
            if (employee is null)
                throw new NotFoundException(nameof(Domain.Entities.User.Employee), request.Id.ToString());


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


            await _employeeRepository.SaveChanges();
        }
    }
}
