using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.CommonCQRS;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Commands.CreateCompensationByManager
{
    public class CreateCompensationByManagerCommandHandler : IRequestHandler<CreateCompensationByManagerCommand, int>
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public CreateCompensationByManagerCommandHandler(ICompensationRepository compensationRepository,
                IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _compensationRepository = compensationRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateCompensationByManagerCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.RequestedForEmployeeId);
            if (employee == null)
                throw new NotFoundException("Employee not found.", request.RequestedForEmployeeId.ToString());

            if (!await EmployeeHelper.IsEmployeeUnderManager(request.RequestedForEmployeeId, request.RequestedByEmployeeId, _employeeRepository))
                throw new ForbidException($"You are not authorized to create compensation request for employee id={request.RequestedForEmployeeId}");

            var newCompensation = _mapper.Map<CompensationRequest>(request);
            newCompensation.SetCompensation(request.IsMultiplied);

            var canSettle = employee.OvertimeSummary.CanSettleOvertime(newCompensation.CompensatedTime);
            if (!canSettle)
                throw new BadRequestException("Insufficient unsettled overtime to settle the requested time.");

            var id = await _compensationRepository.CreateCompensationAsync(newCompensation);
            return id;
        }
    }

}
