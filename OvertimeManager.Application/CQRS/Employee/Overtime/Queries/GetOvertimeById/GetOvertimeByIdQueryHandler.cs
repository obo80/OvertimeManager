using AutoMapper;
using MediatR;
using OvertimeManager.Application.CQRS.Employee.Overtime.DTOs;
using OvertimeManager.Application.CQRS.Employee.Overtime.Queries.GetOvertimeById;
using OvertimeManager.Domain.Exceptions;
using OvertimeManager.Domain.Interfaces;

public class GetOvertimeByIdQueryHandler : IRequestHandler<GetOvertimeByIdQuery, GetOvertimeDto>
{
    private readonly IOvertimeRepository _overtimeRepository;
    private readonly IMapper _mapper;

    public GetOvertimeByIdQueryHandler(IOvertimeRepository overtimeRepository, IMapper mapper)
    {
        _overtimeRepository = overtimeRepository;
        _mapper = mapper;
    }
    public async Task<GetOvertimeDto> Handle(GetOvertimeByIdQuery request, CancellationToken cancellationToken)
    {
        var overtime = await _overtimeRepository.GetByIdAsync(request.OvertimeId);
        if (overtime == null)
            throw new NotFoundException("Overtime request not found.", request.OvertimeId.ToString());

        var overtimeEmployeeId = overtime.RequestedForEmployeeId;
        if (overtimeEmployeeId != request.CurrentEmployeeId)
            throw new UnauthorizedException("You are not authorized to update this overtime request.");

        var updatedOvertimeDto = _mapper.Map<GetOvertimeDto>(overtime);
        return updatedOvertimeDto;
    }
}