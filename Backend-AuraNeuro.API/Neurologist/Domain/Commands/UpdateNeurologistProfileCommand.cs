namespace Backend_AuraNeuro.API.Neurologist.Domain.Model.Commands;

/// <summary>
/// Command to update the full profile (address, specialties, verification).
/// We will use it for the PATCH.
/// </summary>
public record UpdateNeurologistProfileCommand(
    long NeurologistId,
    string Email,
    string Phone,
    string Street,
    string Number,
    string City,
    string PostalCode,
    string Country,
    string Specialties,
    string VerificationStatus);