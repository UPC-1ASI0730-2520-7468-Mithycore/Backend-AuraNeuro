namespace Backend_AuraNeuro.API.Neurologist.Domain.Model.Commands;

/// <summary>
/// Command to register a neurologist's profile
/// </summary>
public record CreateNeurologistCommand(
    Guid UserId,
    string FirstName,
    string LastName,
    string LicenseNumber,
    string Email,
    string Phone,
    string Street,
    string Number,
    string City,
    string PostalCode,
    string Country,
    string Specialties,
    string VerificationStatus = "pending");