namespace Backend_AuraNeuro.API.Neurologist.Interface.REST.Resources;

/// <summary>
/// Resource to update the neurologist's profile (PATCH).
/// </summary>
public record UpdateNeurologistProfileResource(
    string Email,
    string Phone,
    string Street,
    string Number,
    string City,
    string PostalCode,
    string Country,
    string Specialties,
    string VerificationStatus);