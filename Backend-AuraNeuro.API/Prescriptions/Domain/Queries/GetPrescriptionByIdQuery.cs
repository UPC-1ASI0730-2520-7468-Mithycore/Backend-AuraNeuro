// Summary: Query object to load a single Prescription by ID.
namespace Backend_AuraNeuro.API.Prescriptions.Domain.Queries;

public record GetPrescriptionByIdQuery(long PrescriptionId);