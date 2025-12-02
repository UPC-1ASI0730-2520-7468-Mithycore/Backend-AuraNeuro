namespace Backend_AuraNeuro.API.Patient.Domain.Command;

/// <summary>
/// Command to register a new patient.
/// </summary>
public record CreatePatientCommand(
    long UserId,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime BirthDate,
    string? Street = null,
    string? City = null,
    string? Country = null
);