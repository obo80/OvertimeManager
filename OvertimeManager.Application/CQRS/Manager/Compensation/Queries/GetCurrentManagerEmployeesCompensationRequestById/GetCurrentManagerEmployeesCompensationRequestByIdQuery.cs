using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.CommonCQRS;
using OvertimeManager.Application.CQRS.Employee.Compensation.DTOs;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Queries.GetCurrentManagerEmployeesCompensationRequestById
{
    public class GetCurrentManagerEmployeesCompensationRequestByIdQuery : IRequest<GetCompensationDto>
    {
        public GetCurrentManagerEmployeesCompensationRequestByIdQuery(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            CompensationId = id;
        }

        public int CurrentManagerId { get; }
        public int CompensationId { get; }
    }

    public class GetCurrentManagerEmployeesCompensationRequestByIdQueryHandler
        : IRequestHandler<GetCurrentManagerEmployeesCompensationRequestByIdQuery, GetCompensationDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompensationRepository _compensationRepository;
        private readonly IMapper _mapper;

        public GetCurrentManagerEmployeesCompensationRequestByIdQueryHandler(IEmployeeRepository employeeRepository,
            ICompensationRepository compensationRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _compensationRepository = compensationRepository;
            _mapper = mapper;
        }
        public async Task<GetCompensationDto> Handle(GetCurrentManagerEmployeesCompensationRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var compensation = await _compensationRepository.GetByIdAsync(request.CompensationId);

            if (compensation == null || await EmployeeHelper.IsManagerEmployeeRequest(compensation,
                    request.CompensationId, request.CurrentManagerId, _employeeRepository))
                throw new UnauthorizedException("You are not authorized to get this overtime request.");


            var getCompensationDto = _mapper.Map<GetCompensationDto>(compensation);
            return getCompensationDto;
        }
    }
}
