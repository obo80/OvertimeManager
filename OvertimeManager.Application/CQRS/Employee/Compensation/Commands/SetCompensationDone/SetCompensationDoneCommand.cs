using MediatR;
using OvertimeManager.Domain.Constants;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;
using InvalidOperationException = OvertimeManager.Domain.Exceptions.InvalidOperationException;

namespace OvertimeManager.Api.Controllers
{
    public class SetCompensationDoneCommand : IRequest
    {
        public int CurrentEmployeeId { get; }
        public int CompensationId { get; }

        public SetCompensationDoneCommand(int currentEmployeeId, int id)
        {
            CurrentEmployeeId = currentEmployeeId;
            CompensationId = id;
        }
    }

    class SetCompensationDoneCommandHandler : IRequestHandler<SetCompensationDoneCommand>
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public SetCompensationDoneCommandHandler(ICompensationRepository compensationRepository, 
            IEmployeeRepository employeeRepository)
        {
            _compensationRepository = compensationRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task Handle(SetCompensationDoneCommand request, CancellationToken cancellationToken)
        {
            var compensation = await _compensationRepository.GetByIdAsync(request.CompensationId);
            if (compensation == null)
                throw new NotFoundException("Compensation request not found.", request.CompensationId.ToString());

            var compensationEmployeeId = compensation.RequestedForEmployeeId;
            if (compensationEmployeeId != request.CurrentEmployeeId)
                throw new UnauthorizedException("You are not authorized to update this compensation request.");

            var employee = await _employeeRepository.GetByIdAsync(compensation.RequestedForEmployeeId);
            if (employee == null)
                throw new NotFoundException("Employee not found.", compensation.RequestedForEmployeeId.ToString());

            if (compensation.Status != ((StatusEnum)StatusEnum.Approved).ToString())
                throw new InvalidOperationException("Only approved compensation requests can be done.");


            compensation.Status = ((StatusEnum)StatusEnum.Done).ToString();
            await _compensationRepository.SaveChangesAsync();

            if (compensation.RequestedTime > 0)
            {
                employee.OvertimeSummary.SettleOvertime(compensation.RequestedTime);
                await _employeeRepository.SaveChangesAsync();
            }
        }
    }
}