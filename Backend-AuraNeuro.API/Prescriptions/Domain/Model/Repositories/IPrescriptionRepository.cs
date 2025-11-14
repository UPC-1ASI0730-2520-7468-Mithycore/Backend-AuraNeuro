// Summary: Domain repository abstraction for the Prescription aggregate.
using Backend_AuraNeuro.API.Prescriptions.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.Shared.Domain.Repositories;

using PrescriptionAggregate = Backend_AuraNeuro.API.Prescriptions.Domain.Model.Aggregates.Prescription;

namespace Backend_AuraNeuro.API.Prescriptions.Domain.Model.Repositories;

public interface IPrescriptionRepository : IBaseRepository<PrescriptionAggregate>
{
    Task<IEnumerable<PrescriptionAggregate>> FindByPatientIdAsync(Guid patientId);
    Task<IEnumerable<PrescriptionAggregate>> FindByNeurologistIdAsync(Guid neurologistId);
}