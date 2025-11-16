using Backend_AuraNeuro.API.Patient.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.Shared.Domain.Repositories;

namespace Backend_AuraNeuro.API.Patient.Domain.Repositories;

/// <summary>
/// Patient repository.
/// </summary>
public interface IPatientRepository : IBaseRepository<Model.Aggregates.Patient>
{
    /// <summary>
    /// Finds a patient by the associated UserId (login identity).
    /// </summary>
    Task<Model.Aggregates.Patient?> FindByUserIdAsync(Guid userId);

    /// <summary>
    /// Finds a patient by email address.
    /// </summary>
    Task<Model.Aggregates.Patient?> FindByEmailAsync(string email);
    
    Task<bool> AssignNeurologistAsync(long patientId, long neurologistId);
    Task<bool> RemoveNeurologistAsync(long patientId, long neurologistId);
}