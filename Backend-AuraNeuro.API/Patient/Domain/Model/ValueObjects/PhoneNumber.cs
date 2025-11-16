namespace Backend_AuraNeuro.API.Patient.Domain.Model.ValueObjects;

/// <summary>
/// Patient's phone number.
/// </summary>
public record PhoneNumber(string Number)
{
    public PhoneNumber() : this(string.Empty) { }
}