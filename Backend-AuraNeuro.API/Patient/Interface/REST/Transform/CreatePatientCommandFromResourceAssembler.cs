using Backend_AuraNeuro.API.Patient.Domain.Command;
using Backend_AuraNeuro.API.Patient.Interface.REST.Resources;

namespace Backend_AuraNeuro.API.Patient.Interface.REST.Transform;

/// <summary>
/// Maps CreatePatientResource → CreatePatientCommand
/// </summary>
public static class CreatePatientCommandFromResourceAssembler
{
    public static CreatePatientCommand ToCommandFromResource(CreatePatientResource resource) =>
        new(
            resource.UserId,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.PhoneNumber,
            resource.BirthDate,
            resource.Street,
            resource.City,
            resource.Country
        );
}