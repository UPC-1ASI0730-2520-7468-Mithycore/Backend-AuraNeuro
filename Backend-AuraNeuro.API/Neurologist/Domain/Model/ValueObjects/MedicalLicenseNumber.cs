namespace Backend_AuraNeuro.API.Neurologist.Domain.Model.ValueObjects;

/// <summary>
/// Medical license / registration number.
/// </summary>
public class MedicalLicenseNumber
{
    public string Value { get; }

    public MedicalLicenseNumber(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Basic validation (you can extend it with regex, length, etc.).
    /// </summary>
    public MedicalLicenseNumber EnsureValid()
    {
        if (string.IsNullOrWhiteSpace(Value))
            throw new ArgumentException("License number is required", nameof(Value));

        return this;
    }

    public override string ToString() => Value;
}