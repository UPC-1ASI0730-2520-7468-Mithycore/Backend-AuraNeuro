namespace Backend_AuraNeuro.API.Neurologist.Domain.Model.ValueObjects;

/// <summary>
/// Address of the clinic/office.
/// </summary>
public record ClinicAddress(
    string Street,
    string Number,
    string City,
    string PostalCode,
    string Country)
{
    public ClinicAddress() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty) { }

    public string FullAddress => $"{Street} {Number}, {City}, {PostalCode}, {Country}".Trim();
}