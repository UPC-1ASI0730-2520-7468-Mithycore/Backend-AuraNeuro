namespace Backend_AuraNeuro.API.Patient.Domain.Model.ValueObjects;

/// <summary>
/// Patient email address.
/// </summary>
public record EmailAddress(string Address)
{
    public EmailAddress() : this(string.Empty) { }
}