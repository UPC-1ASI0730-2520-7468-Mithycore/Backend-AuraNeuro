// Summary: Command to create a new Prescription aggregate.
namespace Backend_AuraNeuro.API.Prescriptions.Domain.Commands;

public record CreatePrescriptionCommand(
    Guid PatientId,
    Guid NeurologistId,
    IEnumerable<MedicineItemCommandItem> Medicines,
    DateTimeOffset IssuedAt
);