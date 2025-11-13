namespace Backend_AuraNeuro.API.Neurologist.Interfaces.REST.Resources;

/// <summary>
/// Resource to register a neurologist.
/// </summary>
public record CreateNeurologistResource(
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
    string Specialties);