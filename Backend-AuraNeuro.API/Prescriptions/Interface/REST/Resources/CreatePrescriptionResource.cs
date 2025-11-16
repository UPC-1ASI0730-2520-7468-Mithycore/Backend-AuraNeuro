// Summary: Request body for creating a new Prescription.
namespace Backend_AuraNeuro.API.Prescriptions.Interface.REST.Resources;

public class CreatePrescriptionResource
{
    public Guid PatientId { get; set; }
    public Guid NeurologistId { get; set; }
    public DateTimeOffset IssuedAt { get; set; }

    public IList<MedicineItemResource> Medicines { get; set; } = new List<MedicineItemResource>();
}