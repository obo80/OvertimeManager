using AutoMapper;
using MediatR;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Interfaces;

namespace OvertimeManager.Application.CQRS.Employee.Overtime.Commands.CreateOvertime
{
    public class CreateOvertimeCommandHandler : IRequestHandler<CreateOvertimeCommand, int>
    {
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly IMapper _mapper;

        public CreateOvertimeCommandHandler(IOvertimeRepository overtimeRepository, IMapper mapper)
        {
            _overtimeRepository = overtimeRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateOvertimeCommand request, CancellationToken cancellationToken)
        {
            var newOvertime = _mapper.Map<OvertimeRequest>(request);

            var id = await _overtimeRepository.CreateOvertimeAsync(newOvertime);
            return id;
        }
    }
}
