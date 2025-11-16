namespace Backend_AuraNeuro.API.Patient.Domain.Command;

/// <summary>
/// Command to update a patient's contact information and address.
/// Used for PATCH operations.
/// </summary>
public record UpdatePatientProfileCommand(
    long PatientId,
    string Email,
    string PhoneNumber,
    string? Street,
    string? City,
    string? Country
);