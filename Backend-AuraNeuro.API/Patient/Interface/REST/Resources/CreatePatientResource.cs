namespace Backend_AuraNeuro.API.Patient.Interface.REST.Resources;

/// <summary>
/// Resource to register a patient.
/// </summary>
public record CreatePatientResource(
    long UserId,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime BirthDate,
    string? Street,
    string? City,
    string? Country
);