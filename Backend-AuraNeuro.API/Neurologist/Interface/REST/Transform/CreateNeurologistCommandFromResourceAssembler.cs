using Backend_AuraNeuro.API.Neurologist.Domain.Model.Commands;
using Backend_AuraNeuro.API.Neurologist.Interfaces.REST.Resources;

namespace Backend_AuraNeuro.API.Neurologist.Interfaces.REST.Transform;

public static class CreateNeurologistCommandFromResourceAssembler
{
    public static CreateNeurologistCommand ToCommandFromResource(CreateNeurologistResource resource) =>
        new(
            resource.UserId,
            resource.FirstName,
            resource.LastName,
            resource.LicenseNumber,
            resource.Email,
            resource.Phone,
            resource.Street,
            resource.Number,
            resource.City,
            resource.PostalCode,
            resource.Country,
            resource.Specialties);
}