namespace Backend_AuraNeuro.API.Appointments.Domain.Repositories;

using Backend_AuraNeuro.API.Appointments.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.Shared.Domain.Repositories;

public interface IAppointmentRepository : IBaseRepository<Appointment>
{
    Task<IEnumerable<Appointment>> FindByNeurologistIdAsync(long neurologistId);
}