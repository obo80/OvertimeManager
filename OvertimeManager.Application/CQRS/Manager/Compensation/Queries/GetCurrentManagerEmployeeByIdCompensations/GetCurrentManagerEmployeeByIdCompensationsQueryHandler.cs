using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.CommonCQRS;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeeByIdCompensations
{
    public class GetCurrentManagerEmployeeByIdCompensationsQueryHandler :
        IRequestHandler<GetCurrentManagerEmployeeByIdCompensationsQuery, IEnumerable<GetCompensationDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompensationRepository _compensationRepository;
        private readonly IMapper _mapper;

        public GetCurrentManagerEmployeeByIdCompensationsQueryHandler(IEmployeeRepository employeeRepository,
            ICompensationRepository compensationRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _compensationRepository = compensationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetCompensationDto>> Handle(GetCurrentManagerEmployeeByIdCompensationsQuery request, CancellationToken cancellationToken)
        {
            var employee = await EmployeeHelper.GetEmployeeIfUnderManager(request.EmployeeId, request.CurrentManagerId, _employeeRepository);
            var compensations = await _compensationRepository.GetAllForEmployeeIdAsync(employee.Id);

            return _mapper.Map<IEnumerable<GetCompensationDto>>(compensations);
        }
    }


}