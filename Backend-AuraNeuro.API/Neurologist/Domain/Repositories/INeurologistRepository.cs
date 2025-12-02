using Backend_AuraNeuro.API.Shared.Domain.Repositories;
using NeurologistProfile = Backend_AuraNeuro.API.Neurologist.Domain.Model.Aggregates.Neurologist;

namespace Backend_AuraNeuro.API.Neurologist.Domain.Repositories;

/// <summary>
/// Neurologist repository.
/// </summary>
public interface INeurologistRepository : IBaseRepository<NeurologistProfile>
{
    Task<NeurologistProfile?> FindByUserIdAsync(long userId);

    Task<NeurologistProfile?> FindByLicenseNumberAsync(string licenseNumber);

    Task<IEnumerable<NeurologistProfile>> FindBySpecialtyAsync(string specialty);
}