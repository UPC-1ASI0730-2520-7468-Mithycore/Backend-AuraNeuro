namespace Backend_AuraNeuro.API.Appointments.Application.Internal.CommandServices;

using Backend_AuraNeuro.API.Appointments.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.Appointments.Domain.Model.Commands;
using Backend_AuraNeuro.API.Appointments.Domain.Repositories;
using Backend_AuraNeuro.API.Appointments.Domain.Services.Command;
using Backend_AuraNeuro.API.Shared.Domain.Repositories;

public class AppointmentCommandService : IAppointmentCommandService
{
    private readonly IAppointmentRepository repository;
    private readonly IUnitOfWork unitOfWork;

    public AppointmentCommandService(IAppointmentRepository repository, IUnitOfWork unitOfWork)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Appointment?> Handle(CreateAppointmentCommand command)
    {
        var appointment = new Appointment(command);

        await repository.AddAsync(appointment);
        await unitOfWork.CompleteAsync();

        return appointment;
    }
}