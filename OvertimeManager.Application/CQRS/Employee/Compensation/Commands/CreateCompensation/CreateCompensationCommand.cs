using AutoMapper;
using MediatR;
using OvertimeManager.Domain.Constants;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;
using InvalidOperationException = OvertimeManager.Domain.Exceptions.InvalidOperationException;

namespace OvertimeManager.Api.Controllers
{
    public class CreateCompensationCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateOnly CreatedForDay { get; set; } = DateOnly.FromDateTime(DateTime.Now); //default to today
        public int RequestedByEmployeeId { get; set; }
        public int RequestedForEmployeeId { get; set; }

        public double RequestedTime { get; set; }

        //public int ApprovalStatusId { get; set; } = 1;      //default to "Pending"
        public string Status { get; set; } = ((StatusEnum)StatusEnum.Pending).ToString();

        public bool IsMultiplied { get; set; } = false; // Employee requests are not multiplied

        public CreateCompensationCommand(int currentEmployeeId)
        {
            RequestedByEmployeeId = currentEmployeeId;
            RequestedForEmployeeId = currentEmployeeId;
        }

    }
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
                throw new NotFoundException("Employee not found.", request.RequestedForEmployeeId.ToString());

            var newCompensation = _mapper.Map<CompensationRequest>(request);
            newCompensation.SetCompensation(request.IsMultiplied);

            var canSettle = employee.OvertimeSummary.CanSettleOvertime(newCompensation.CompensatedTime);
            if (!canSettle)
                throw new InvalidOperationException("Insufficient unsettled overtime to settle the requested time.");

            var id = await _compensationRepository.CreateCompensationAsync(newCompensation);
            return id;
        }
    }
}