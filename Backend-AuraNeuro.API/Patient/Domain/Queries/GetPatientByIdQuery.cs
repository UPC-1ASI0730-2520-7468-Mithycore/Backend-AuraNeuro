namespace Backend_AuraNeuro.API.Patient.Domain.Queries;

/// <summary>
/// Query to get a patient by their Id.
/// </summary>
public record GetPatientByIdQuery(long PatientId);