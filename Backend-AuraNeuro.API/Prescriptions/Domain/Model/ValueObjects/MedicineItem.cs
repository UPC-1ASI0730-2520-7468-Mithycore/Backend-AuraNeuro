// Summary: Value object that models a single medicine inside a prescription.
namespace Backend_AuraNeuro.API.Prescriptions.Domain.Model.ValueObjects;

public class MedicineItem
{
    public string Name { get; private set; } = string.Empty;
    public string Dosage { get; private set; } = string.Empty;
    public string Frequency { get; private set; } = string.Empty;

    public MedicineItem(string name, string dosage, string frequency)
    {
        Name = name;
        Dosage = dosage;
        Frequency = frequency;
    }
    
    private MedicineItem() { }
}