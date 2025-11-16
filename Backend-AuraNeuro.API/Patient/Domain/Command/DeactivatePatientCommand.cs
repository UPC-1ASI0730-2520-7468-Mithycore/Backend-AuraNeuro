namespace Backend_AuraNeuro.API.Patient.Domain.Command;

/// <summary>
/// Command to deactivate (soft delete) a patient.
/// </summary>
public record DeactivatePatientCommand(long PatientId);