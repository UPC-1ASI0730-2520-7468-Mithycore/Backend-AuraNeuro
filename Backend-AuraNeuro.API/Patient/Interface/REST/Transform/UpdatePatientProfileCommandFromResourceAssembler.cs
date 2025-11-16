using Backend_AuraNeuro.API.Patient.Domain.Command;
using Backend_AuraNeuro.API.Patient.Interface.REST.Resources;

namespace Backend_AuraNeuro.API.Patient.Interface.REST.Transform;

public static class UpdatePatientProfileCommandFromResourceAssembler
{
    public static UpdatePatientProfileCommand ToCommandFromResource(
        int patientId,
        UpdatePatientProfileResource resource) =>
        new(
            patientId,
            resource.Email,
            resource.PhoneNumber,
            resource.Street,
            resource.City,
            resource.Country
        );
}