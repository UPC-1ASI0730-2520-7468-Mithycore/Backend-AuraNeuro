using Backend_AuraNeuro.API.Neurologist.Domain.Model.Commands;
using Backend_AuraNeuro.API.Neurologist.Interfaces.REST.Resources;

namespace Backend_AuraNeuro.API.Neurologist.Interfaces.REST.Transform;

public static class UpdateNeurologistProfileCommandFromResourceAssembler
{
    public static UpdateNeurologistProfileCommand ToCommandFromResource(
        long neurologistId,
        UpdateNeurologistProfileResource resource) =>
        new(
            neurologistId,
            resource.Email,
            resource.Phone,
            resource.Street,
            resource.Number,
            resource.City,
            resource.PostalCode,
            resource.Country,
            resource.Specialties,
            resource.VerificationStatus);
}