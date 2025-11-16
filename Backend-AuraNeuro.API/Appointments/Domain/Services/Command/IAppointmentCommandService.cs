namespace Backend_AuraNeuro.API.Appointments.Domain.Services.Command;

using Backend_AuraNeuro.API.Appointments.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.Appointments.Domain.Model.Commands;

public interface IAppointmentCommandService
{
    Task<Appointment?> Handle(CreateAppointmentCommand command);
}