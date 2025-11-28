using AutoMapper;
using MediatR;
using OvertimeManager.Domain.Constants;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Manager.Overtime.Commands.RejectCurrentManagerEmployeesOvertimeRequestById
{
    public class RejectCurrentManagerEmployeesOvertimeRequestByIdCommand : IRequest
    {


        public RejectCurrentManagerEmployeesOvertimeRequestByIdCommand(int currentManagerId, int id)
        {
            CurrentManagerId = currentManagerId;
            OvertimeId = id;
        }

        public int CurrentManagerId { get; }
        public int OvertimeId { get; }
    }

    class RejectCurrentManagerEmployeesOvertimeRequestByIdCommandHandler : 
                IRequestHandler<RejectCurrentManagerEmployeesOvertimeRequestByIdCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly IMapper _mapper;

        public RejectCurrentManagerEmployeesOvertimeRequestByIdCommandHandler(
            IEmployeeRepository employeeRepository, IOvertimeRepository overtimeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _overtimeRepository = overtimeRepository;
            _mapper = mapper;
        }
        public async Task Handle(RejectCurrentManagerEmployeesOvertimeRequestByIdCommand request, CancellationToken cancellationToken)
        {
            var overtime = await _overtimeRepository.GetByIdAsync(request.OvertimeId);
            if (overtime == null)
                throw new NotFoundException("Overtime request not found.", request.OvertimeId.ToString());

            var manager = await _employeeRepository.GetByIdAsync(request.CurrentManagerId);
            if (manager == null)
                throw new NotFoundException("Manager not found", request.CurrentManagerId.ToString());

            if (overtime.RequestedForEmployee!.ManagerId != manager.Id)
                throw new UnauthorizedException("You are not authorized to get this overtime request.");

            overtime.Status = StatusEnum.Rejected.ToString();

            await _overtimeRepository.SaveChangesAsync();
        }
    }
}