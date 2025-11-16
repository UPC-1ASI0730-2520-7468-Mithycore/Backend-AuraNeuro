using Backend_AuraNeuro.API.Patient.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.Patient.Interface.REST.Resources;

namespace Backend_AuraNeuro.API.Patient.Interface.REST.Transform;

/// <summary>
/// Maps Patient aggregate to REST resource.
/// </summary>
public static class PatientResourceFromEntityAssembler
{
    public static PatientResource ToResourceFromEntity(Domain.Model.Aggregates.Patient entity) =>
        new(
            entity.Id,
            entity.UserId,
            entity.Name.FullName,
            entity.Email.Address,
            entity.PhoneNumber.Number,
            entity.BirthDate.Date,
            entity.Address?.FullAddress ?? string.Empty,
            entity.IsActive
        );
}