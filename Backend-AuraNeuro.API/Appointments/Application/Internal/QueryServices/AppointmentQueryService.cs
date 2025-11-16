namespace Backend_AuraNeuro.API.Appointments.Application.Internal.QueryServices;

using Backend_AuraNeuro.API.Appointments.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.Appointments.Domain.Model.Queries;
using Backend_AuraNeuro.API.Appointments.Domain.Repositories;
using Backend_AuraNeuro.API.Appointments.Domain.Services.Queries;

public class AppointmentQueryService : IAppointmentQueryService
{
    private readonly IAppointmentRepository repository;

    public AppointmentQueryService(IAppointmentRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Appointment>> Handle(GetAllAppointmentsQuery query)
    {
        return await repository.ListAsync();
    }

    public async Task<Appointment?> Handle(GetAppointmentByIdQuery query)
    {
        return await repository.FindByIdAsync(query.AppointmentId);
    }

    public async Task<IEnumerable<Appointment>> Handle(GetAppointmentsByNeurologistIdQuery query)
    {
        return await repository.FindByNeurologistIdAsync(query.NeurologistId);
    }
}