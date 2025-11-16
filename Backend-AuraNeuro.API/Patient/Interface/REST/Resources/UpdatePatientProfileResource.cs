namespace Backend_AuraNeuro.API.Patient.Interface.REST.Resources;

/// <summary>
/// Resource to update the patient's profile (PATCH).
/// </summary>
public record UpdatePatientProfileResource(
    string Email,
    string PhoneNumber,
    string? Street,
    string? City,
    string? Country
);