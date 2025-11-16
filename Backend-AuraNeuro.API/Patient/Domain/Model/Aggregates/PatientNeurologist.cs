namespace Backend_AuraNeuro.API.Patient.Domain.Model.Aggregates;

public class PatientNeurologist
{
    public int Id { get; set; }

    public int PatientId { get; set; }
    public long NeurologistId { get; set; }

    public bool IsActive { get; set; } = true;
}