using Backend_AuraNeuro.API.Patient.Domain.Command;
using Backend_AuraNeuro.API.Patient.Domain.Model.ValueObjects;

namespace Backend_AuraNeuro.API.Patient.Domain.Model.Aggregates;

/// <summary>
/// Patient aggregate root.
/// </summary>
public partial class Patient
{
    public long Id { get; private set; }
    
    public long UserId { get; private set; }
    
    // 🔗 referencia sencilla al neurólogo
    public long? NeurologistId { get; private set; }
    public PersonName Name { get; private set; }
    public EmailAddress Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public BirthDate BirthDate { get; private set; }
    public Address? Address { get; private set; }

    public bool IsActive { get; private set; } = true;

    // EF Core constructor
    public Patient()
    {
        Name = new PersonName();
        NeurologistId = null;
        Email = new EmailAddress();
        PhoneNumber = new PhoneNumber();
        BirthDate = new BirthDate(DateTime.UtcNow);
        Address = null;
    }

    /// <summary>
    /// Main constructor with complete data.
    /// </summary>
    public Patient(
        long userId,
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        DateTime birthDate,
        string? street,
        string? city,
        string? country)
        : this()
    {
        UserId = userId;
        Name = new PersonName(firstName, lastName);
        Email = new EmailAddress(email);
        PhoneNumber = new PhoneNumber(phoneNumber);
        BirthDate = new BirthDate(birthDate);

        if (!string.IsNullOrWhiteSpace(street) &&
            !string.IsNullOrWhiteSpace(city) &&
            !string.IsNullOrWhiteSpace(country))
        {
            Address = new Address(street, city, country);
        }
    }
    
    public void AssignNeurologist(long neurologistId)
    {
        if (neurologistId <= 0)
            throw new ArgumentException("Invalid neurologist id.", nameof(neurologistId));

        NeurologistId = neurologistId;
    }
    
    public void RemoveNeurologist()
    {
        NeurologistId = null;
    }


    /// <summary>
    /// Constructor from CreatePatientCommand.
    /// </summary>
    public Patient(CreatePatientCommand command)
        : this(
            command.UserId,
            command.FirstName,
            command.LastName,
            command.Email,
            command.PhoneNumber,
            command.BirthDate,
            command.Street,
            command.City,
            command.Country)
    {
    }

    public void UpdateContactInfo(string email, string phoneNumber)
    {
        Email = new EmailAddress(email);
        PhoneNumber = new PhoneNumber(phoneNumber);
    }

    public void UpdateAddress(string? street, string? city, string? country)
    {
        if (!string.IsNullOrWhiteSpace(street) &&
            !string.IsNullOrWhiteSpace(city) &&
            !string.IsNullOrWhiteSpace(country))
        {
            Address = new Address(street, city, country);
        }
    }

    public void Deactivate() => IsActive = false;

    public void Activate() => IsActive = true;
}
