using NeurologistProfile = Backend_AuraNeuro.API.Neurologist.Domain.Model.Aggregates.Neurologist;
using Backend_AuraNeuro.API.Neurologist.Interfaces.REST.Resources;

namespace Backend_AuraNeuro.API.Neurologist.Interfaces.REST.Transform;

/// <summary>
/// Maps Neurologist aggregate to REST resource.
/// </summary>
public static class NeurologistResourceFromEntityAssembler
{
    public static NeurologistResource ToResourceFromEntity(NeurologistProfile entity) =>
        new(
            entity.Id,
            entity.UserId,
            entity.FullName,
            entity.EmailAddress,
            entity.PhoneNumber,
            entity.ClinicFullAddress,
            entity.LicenseNumber.Value,
            entity.Specialties,
            entity.VerificationStatus,
            entity.IsActive);
}