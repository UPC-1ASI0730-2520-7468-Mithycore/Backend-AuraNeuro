// Summary: Aggregate root for prescriptions; holds patient, neurologist and medicines data.
using Backend_AuraNeuro.API.Prescriptions.Domain.Commands;
using Backend_AuraNeuro.API.Prescriptions.Domain.Model.ValueObjects;

namespace Backend_AuraNeuro.API.Prescriptions.Domain.Model.Aggregates;

public partial class Prescription
{
    public long Id { get; private set; }

    public Guid PatientId { get; private set; }
    public Guid NeurologistId { get; private set; }

    public List<MedicineItem> Medicines { get; private set; } = new();

    public DateTimeOffset IssuedAt { get; private set; }

    public string SignatureHash { get; private set; } = string.Empty;

    public bool Revoked { get; private set; }

    // EF Core parameterless constructor
    public Prescription()
    {
        IssuedAt = DateTimeOffset.UtcNow;
    }

    // Main ctor
    public Prescription(Guid patientId, Guid neurologistId,
        IEnumerable<MedicineItem> medicines, DateTimeOffset issuedAt) : this()
    {
        PatientId = patientId;
        NeurologistId = neurologistId;
        IssuedAt = issuedAt;
        Medicines = medicines.ToList();
    }

    // Ctor from command (same pattern as in your prof's project)
    public Prescription(CreatePrescriptionCommand command) : this(
        command.PatientId,
        command.NeurologistId,
        command.Medicines.Select(m => new MedicineItem(m.Name, m.Dosage, m.Frequency)),
        command.IssuedAt)
    { }

    public void UpdateMedicines(IEnumerable<MedicineItem> medicines)
    {
        Medicines = medicines.ToList();
    }

    public void Revoke()
    {
        Revoked = true;
    }
}