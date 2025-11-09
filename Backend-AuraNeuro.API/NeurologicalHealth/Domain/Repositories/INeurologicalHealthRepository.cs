using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.Shared.Domain.Repositories;

namespace Backend_AuraNeuro.API.NeurologicalHealth.Domain.Repositories;

public interface INeurologicalHealthRepository: IBaseRepository<NeuroAssessment>
{
    Task<IEnumerable<NeuroAssessment>> FindByPatientIdAsync(long patientId);
    Task<IEnumerable<NeuroAssessment>> FindByNeurologistIdAsync(long neurologistId);
}