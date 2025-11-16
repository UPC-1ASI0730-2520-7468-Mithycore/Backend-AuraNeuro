// Summary: Query object to list prescriptions by neurologist ID.
namespace Backend_AuraNeuro.API.Prescriptions.Domain.Queries;

public record GetPrescriptionsByNeurologistIdQuery(Guid NeurologistId);