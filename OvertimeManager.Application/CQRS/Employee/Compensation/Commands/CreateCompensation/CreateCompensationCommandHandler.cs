using AutoMapper;
using MediatR;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Compensation.Commands.CreateCompensation
{
    public class CreateCompensationCommandHandler : IRequestHandler<CreateCompensationCommand, int>
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public CreateCompensationCommandHandler(ICompensationRepository compensationRepository, 
                IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _compensationRepository = compensationRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateCompensationCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.RequestedForEmployeeId);
            if (employee == null)
                throw new NotFoundException("Employee", request.RequestedForEmployeeId.ToString());

            var newCompensation = _mapper.Map<CompensationRequest>(request);
            newCompensation.SetCompensation(request.IsMultiplied);

            var canSettle = employee.OvertimeSummary!.CanSettleOvertime(newCompensation.CompensatedTime);
            if (!canSettle)
                throw new BadRequestException("Insufficient unsettled overtime to settle the requested time.");

            var id = await _compensationRepository.CreateCompensationAsync(newCompensation);
            return id;
        }
    }
}