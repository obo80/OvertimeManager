using AutoMapper;
using FluentValidation;
using MediatR;
using OvertimeManager.Application.CQRS.CommonCQRS;
using OvertimeManager.Application.CQRS.Manager.Compensation.DTOs;
using OvertimeManager.Domain.Constants;
using OvertimeManager.Domain.Entities.Overtime;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvalidOperationException = OvertimeManager.Domain.Exceptions.InvalidOperationException;

namespace OvertimeManager.Application.CQRS.Manager.Compensation.Commands.CreateCompensationByManager
{
    public class CreateCompensationByManagerCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateOnly CreatedForDay { get; set; } = DateOnly.FromDateTime(DateTime.Now); //default to today
        public int RequestedByEmployeeId { get; set; }
        public int RequestedForEmployeeId { get; set; }

        public double RequestedTime { get; set; }
        public string Status { get; set; } = ((StatusEnum)StatusEnum.Approved).ToString();
        public DateTime ApprovedAt { get; set; } =  DateTime.Now;
        public int ApprovedByEmployeeId { get; set; } 
        public string approvedByEmployeeEmail { get; set; }

        public bool IsMultiplied { get; set; } = true; // Manager requests are multiplied

        public CreateCompensationByManagerCommand(int currentManagerId)
        {
            RequestedByEmployeeId = currentManagerId;
            ApprovedByEmployeeId = currentManagerId;
        }
    }

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
                throw new UnauthorizedException($"You are not authorized to create compensation request for employee id={request.RequestedForEmployeeId}");

            var newCompensation = _mapper.Map<CompensationRequest>(request);
            newCompensation.SetCompensation(request.IsMultiplied);

            var canSettle = employee.OvertimeSummary.CanSettleOvertime(newCompensation.CompensatedTime);
            if (!canSettle)
                throw new InvalidOperationException("Insufficient unsettled overtime to settle the requested time.");

            var id = await _compensationRepository.CreateCompensationAsync(newCompensation);
            return id;
        }
    }

    public class CreateCompensationByManagerCommandValidator : AbstractValidator<CreateCompensationByManagerDto>
    {
        public CreateCompensationByManagerCommandValidator()
        {
            RuleFor(dto => dto.RequestedTime)
                .GreaterThan(0).WithMessage("Requested time must be greater than zero");
            RuleFor(dto => dto.RequestedForEmployeeId)
                .GreaterThan(0).WithMessage("Required Employee Id must be greater than zero");
        }

    }

}
