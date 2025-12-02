namespace Backend_AuraNeuro.API.Patient.Domain.Queries;

/// <summary>
/// Query to get a patient by their email.
/// </summary>
public record GetPatientByUserIdQuery(long UserId);