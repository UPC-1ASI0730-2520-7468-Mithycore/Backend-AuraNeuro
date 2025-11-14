// Summary: Query object to list prescriptions by patient ID.
namespace Backend_AuraNeuro.API.Prescriptions.Domain.Queries;

public record GetPrescriptionsByPatientIdQuery(Guid PatientId);