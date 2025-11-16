namespace Backend_AuraNeuro.API.Neurologist.Domain.Model.ValueObjects;

/// <summary>
/// Person's name.
/// </summary>
public record PersonName(string FirstName, string LastName)
{
    public PersonName() : this(string.Empty, string.Empty) { }

    public string FullName => $"{FirstName} {LastName}".Trim();
}