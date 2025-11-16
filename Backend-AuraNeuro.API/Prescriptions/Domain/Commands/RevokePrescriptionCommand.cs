// Summary: Command to revoke (soft delete) an existing Prescription.
namespace Backend_AuraNeuro.API.Prescriptions.Domain.Commands;

public record RevokePrescriptionCommand(long PrescriptionId);