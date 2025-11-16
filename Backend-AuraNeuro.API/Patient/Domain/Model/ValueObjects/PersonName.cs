namespace Backend_AuraNeuro.API.Patient.Domain.Model.ValueObjects;

/// <summary>
/// Patient's full name.
/// </summary>
public record PersonName(string FirstName, string LastName)
{
    public PersonName() : this(string.Empty, string.Empty) { }

    public string FullName => $"{FirstName} {LastName}".Trim();
}