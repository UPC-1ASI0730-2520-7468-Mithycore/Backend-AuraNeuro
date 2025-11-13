using Backend_AuraNeuro.API.Appointment.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.Shared.Domain.Repositories;


namespace Backend_AuraNeuro.API.Appointment.Domain.Repositories;

public interface IAppointmentRepository : IBaseRepository<Model.Aggregates.Appointment>
{
    Task<IEnumerable<Model.Aggregates.Appointment>> FindByNeurologistIdAsync(long neurologistId);
}