namespace Backend_AuraNeuro.API.Neurologist.Domain.Model.ValueObjects;

/// <summary>
/// Neurologist's phone number.
/// </summary>
public record PhoneNumber(string Number)
{
    public PhoneNumber() : this(string.Empty) { }
}