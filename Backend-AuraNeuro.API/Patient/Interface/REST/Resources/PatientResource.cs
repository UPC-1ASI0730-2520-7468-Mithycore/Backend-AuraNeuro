namespace Backend_AuraNeuro.API.Patient.Interface.REST.Resources;

/// <summary>
/// Resource to update the neurologist's profile (PATCH).
/// </summary>
public record PatientResource(
    long Id,
    Guid UserId,
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime BirthDate,
    string Address,
    bool IsActive
);