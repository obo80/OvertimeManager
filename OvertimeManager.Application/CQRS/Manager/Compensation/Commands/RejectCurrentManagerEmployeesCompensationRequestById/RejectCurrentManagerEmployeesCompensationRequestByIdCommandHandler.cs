using AutoMapper;
using MediatR;
using OvertimeManager.Domain.Constants;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Commands.RejectCurrentManagerEmployeesCompensationRequestById
{
    public class RejectCurrentManagerEmployeesCompensationRequestByIdCommandHandler : 
                IRequestHandler<RejectCurrentManagerEmployeesCompensationRequestByIdCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompensationRepository _compensationRepository;
        private readonly IMapper _mapper;

        public RejectCurrentManagerEmployeesCompensationRequestByIdCommandHandler(
            IEmployeeRepository employeeRepository, ICompensationRepository compensationRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _compensationRepository = compensationRepository;
            _mapper = mapper;
        }
        public async Task Handle(RejectCurrentManagerEmployeesCompensationRequestByIdCommand request, 
            CancellationToken cancellationToken)
        {
            var compensation = await _compensationRepository.GetByIdAsync(request.CompensationId);
            if (compensation == null)
                throw new NotFoundException("Compensation request not found.", request.CompensationId.ToString());

            var manager = await _employeeRepository.GetByIdAsync(request.CurrentManagerId);
            if (manager == null)
                throw new NotFoundException("Manager not found", request.CurrentManagerId.ToString());

            if (compensation.RequestedForEmployee!.ManagerId != manager.Id)
                throw new UnauthorizedException("You are not authorized to get this overtime request.");

            compensation.Status = StatusEnum.Rejected.ToString();

            await _compensationRepository.SaveChangesAsync();
        }
    }
}
