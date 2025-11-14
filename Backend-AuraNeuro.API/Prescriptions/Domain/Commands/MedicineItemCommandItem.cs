// Summary: Command-level DTO representing one medicine in the request payload.
namespace Backend_AuraNeuro.API.Prescriptions.Domain.Commands;

public record MedicineItemCommandItem(
    string Name,
    string Dosage,
    string Frequency
);