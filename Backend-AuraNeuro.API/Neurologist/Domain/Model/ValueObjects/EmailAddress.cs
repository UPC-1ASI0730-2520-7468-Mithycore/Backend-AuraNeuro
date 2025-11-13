namespace Backend_AuraNeuro.API.Neurologist.Domain.Model.ValueObjects;

/// <summary>
/// Email address.
/// </summary>
public record EmailAddress(string Address)
{
    public EmailAddress() : this(string.Empty) { }
}