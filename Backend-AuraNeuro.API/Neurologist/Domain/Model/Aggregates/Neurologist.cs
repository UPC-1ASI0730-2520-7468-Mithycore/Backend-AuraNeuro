using Backend_AuraNeuro.API.Neurologist.Domain.Commands;
using Backend_AuraNeuro.API.Neurologist.Domain.Model.ValueObjects;

namespace Backend_AuraNeuro.API.Neurologist.Domain.Model.Aggregates;

/// <summary>
/// Neurologist aggregate root.
/// </summary>
public partial class Neurologist
{
    public long Id { get; }

    public long UserId { get; private set; }

    public PersonName Name { get; private set; }
    public MedicalLicenseNumber LicenseNumber { get; private set; }
    public EmailAddress Email { get; private set; }
    public PhoneNumber Phone { get; private set; }
    public ClinicAddress Address { get; private set; }

    public string Specialties { get; private set; } = string.Empty;
    public string VerificationStatus { get; private set; } = "pending";

    public bool IsActive { get; private set; } = true;

    public string FullName => Name.FullName;
    public string EmailAddress => Email.Address;
    public string PhoneNumber => Phone.Number;
    public string ClinicFullAddress => Address.FullAddress;

    // EF Core parameterless constructor
    public Neurologist()
    {
        Name = new PersonName();
        LicenseNumber = new MedicalLicenseNumber(string.Empty);
        Email = new EmailAddress();
        Phone = new PhoneNumber();
        Address = new ClinicAddress();
    }

    public Neurologist(
        long userId,
        string firstName,
        string lastName,
        string licenseNumber,
        string email,
        string phone,
        string street,
        string number,
        string city,
        string postalCode,
        string country,
        string specialties,
        string verificationStatus = "pending")
        : this()
    {
        UserId = userId;
        Name = new PersonName(firstName, lastName);
        LicenseNumber = new MedicalLicenseNumber(licenseNumber).EnsureValid();
        Email = new EmailAddress(email);
        Phone = new PhoneNumber(phone);
        Address = new ClinicAddress(street, number, city, postalCode, country);
        Specialties = specialties;
        VerificationStatus = verificationStatus;
    }

    public Neurologist(CreateNeurologistCommand command)
        : this(
            command.UserId,
            command.FirstName,
            command.LastName,
            command.LicenseNumber,
            command.Email,
            command.Phone,
            command.Street,
            command.Number,
            command.City,
            command.PostalCode,
            command.Country,
            command.Specialties,
            command.VerificationStatus)
    {
    }

    public void UpdateContact(string email, string phone)
    {
        Email = new EmailAddress(email);
        Phone = new PhoneNumber(phone);
    }

    public void UpdateAddress(string street, string number, string city, string postalCode, string country)
    {
        Address = new ClinicAddress(street, number, city, postalCode, country);
    }

    public void UpdateProfessionalInfo(string licenseNumber, string specialties, string verificationStatus)
    {
        LicenseNumber = new MedicalLicenseNumber(licenseNumber).EnsureValid();
        Specialties = specialties;
        VerificationStatus = verificationStatus;
    }

    public void Deactivate() => IsActive = false;

    public void Activate() => IsActive = true;
}
