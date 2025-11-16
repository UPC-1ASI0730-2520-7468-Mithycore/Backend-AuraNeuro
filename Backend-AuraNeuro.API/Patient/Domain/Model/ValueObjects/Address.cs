namespace Backend_AuraNeuro.API.Patient.Domain.Model.ValueObjects;

/// <summary>
/// Patient home address (simplified version).
/// </summary>
public record Address(string Street, string City, string Country)
{
    public Address() : this(string.Empty, string.Empty, string.Empty) { }

    public string FullAddress => $"{Street}, {City}, {Country}".Trim();
}