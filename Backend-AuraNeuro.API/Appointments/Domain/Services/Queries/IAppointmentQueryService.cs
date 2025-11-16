namespace Backend_AuraNeuro.API.Appointments.Domain.Services.Queries;

using Backend_AuraNeuro.API.Appointments.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.Appointments.Domain.Model.Queries;

public interface IAppointmentQueryService
{
    Task<IEnumerable<Appointment>> Handle(GetAllAppointmentsQuery query);
    Task<Appointment?> Handle(GetAppointmentByIdQuery query);
    Task<IEnumerable<Appointment>> Handle(GetAppointmentsByNeurologistIdQuery query);
}