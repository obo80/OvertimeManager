using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.HR.Employees.DTOs;
using OvertimeManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.CQRS.HR.Employees.Queries.GetAllEmployees
{
    internal class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeWithOvetimeDataDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeWithOvetimeDataDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<EmployeeWithOvetimeDataDto>>(employees);

            return dtos;
        }
    }
}
