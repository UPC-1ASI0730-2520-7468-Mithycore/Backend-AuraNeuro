namespace Backend_AuraNeuro.API.Patient.Domain.Model.ValueObjects;

/// <summary>
/// Represents the birth date of a patient.
/// </summary>
public record BirthDate(DateTime Date)
{
    public BirthDate() : this(DateTime.UtcNow) { }

    public int Age => 
        (int)((DateTime.UtcNow - Date).TotalDays / 365.25);
}