// Summary: REST representation of a Prescription aggregate.
namespace Backend_AuraNeuro.API.Prescriptions.Interface.REST.Resources;

public class PrescriptionResource
{
    public long Id { get; set; }

    public Guid PatientId { get; set; }
    public Guid NeurologistId { get; set; }

    public DateTimeOffset IssuedAt { get; set; }

    public string SignatureHash { get; set; } = string.Empty;

    public bool Revoked { get; set; }

    public IList<MedicineItemResource> Medicines { get; set; } = new List<MedicineItemResource>();
}