using AutoMapper;
using MediatR;
using OvertimeManager.Domain.Constants;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Commands.ApproveCurrentManagerEmployeesCompensationRequestById
{
    public class ApproveCurrentManagerEmployeesCompensationRequestByIdCommandHandler : 
                IRequestHandler<ApproveCurrentManagerEmployeesCompensationRequestByIdCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompensationRepository _compensationRepository;
        private readonly IMapper _mapper;

        public ApproveCurrentManagerEmployeesCompensationRequestByIdCommandHandler(
            IEmployeeRepository employeeRepository, ICompensationRepository compensationRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _compensationRepository = compensationRepository;
            _mapper = mapper;
        }
        public async Task Handle(ApproveCurrentManagerEmployeesCompensationRequestByIdCommand request, CancellationToken cancellationToken)
        {
            var compensation = await _compensationRepository.GetByIdAsync(request.CompensationId);
            if (compensation == null)
                throw new NotFoundException("Compensation request not found.", request.CompensationId.ToString());

            var manager = await _employeeRepository.GetByIdAsync(request.CurrentManagerId);
            if (manager == null)
                throw new NotFoundException("Manager not found", request.CurrentManagerId.ToString());


            var employeeId = compensation.RequestedForEmployeeId;
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
                throw new NotFoundException("Employee not found.", employeeId.ToString());

            if (employee.ManagerId != manager.Id)
                throw new UnauthorizedException("You are not authorized to get this overtime request.");

            var canSettle = employee.OvertimeSummary.CanSettleOvertime(compensation.CompensatedTime);
            if (!canSettle)
                throw new Domain.Exceptions.InvalidOperationException("Insufficient unsettled overtime to settle the requested time.");

            compensation.Status = StatusEnum.Approved.ToString();
            compensation.ApprovedAt = DateTime.Now;
            compensation.ApprovedByEmployeeId = manager.Id;

            await _compensationRepository.SaveChangesAsync();
        }
    }
}