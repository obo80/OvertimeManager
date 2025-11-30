using MediatR;
using OvertimeManager.Domain.Constants;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Api.Controllers
{
    public class SetOvertimeDoneCommandHandler : IRequestHandler<SetOvertimeDoneCommand>
    {
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public SetOvertimeDoneCommandHandler(IOvertimeRepository overtimeRepository, IEmployeeRepository employeeRepository)
        {
            _overtimeRepository = overtimeRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task Handle(SetOvertimeDoneCommand request, CancellationToken cancellationToken)
        {
            var overtime = await _overtimeRepository.GetByIdAsync(request.OvertimeId);
            if (overtime == null)
                throw new NotFoundException("Overtime request not found.", request.OvertimeId.ToString());

            var overtimeEmployeeId = overtime.RequestedForEmployeeId;
            if (overtimeEmployeeId != request.CurrentEmployeeId)
                throw new UnauthorizedException("You are not authorized to update this overtime request.");

            var employee = await _employeeRepository.GetByIdAsync(overtime.RequestedForEmployeeId);
            if (employee == null)
                throw new NotFoundException("Employee not found.", overtime.RequestedForEmployeeId.ToString());

            if (overtime.Status == ((StatusEnum)StatusEnum.Done).ToString())
                throw new Domain.Exceptions.InvalidOperationException("This request was already done.");

            if (overtime.Status != ((StatusEnum)StatusEnum.Approved).ToString())
                throw new Domain.Exceptions.InvalidOperationException("Only approved overtime requests can be done.");


            double actualTime = 0.0;
            if (request.ActualTime != null)
                actualTime = (double)request.ActualTime;
            else
                actualTime = overtime.RequestedTime;

            overtime.ActualTime = actualTime;
            overtime.Status = ((StatusEnum)StatusEnum.Done).ToString();
            await _overtimeRepository.SaveChangesAsync();

            //update employee's overtime hours
            if (overtime.ActualTime.HasValue)
                employee.OvertimeSummary.AddTakenOvertime(actualTime);
            await _employeeRepository.SaveChangesAsync();
        }
    }
}