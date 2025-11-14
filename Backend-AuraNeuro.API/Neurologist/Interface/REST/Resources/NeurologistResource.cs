namespace Backend_AuraNeuro.API.Neurologist.Interface.REST.Resources;

/// <summary>
/// REST representation of the neurologist.
/// </summary>
public record NeurologistResource(
    long Id,
    Guid UserId,
    string FullName,
    string Email,
    string Phone,
    string ClinicAddress,
    string LicenseNumber,
    string Specialties,
    string VerificationStatus,
    bool IsActive);