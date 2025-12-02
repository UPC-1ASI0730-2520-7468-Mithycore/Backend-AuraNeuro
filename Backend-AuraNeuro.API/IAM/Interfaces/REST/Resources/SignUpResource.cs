namespace Backend_AuraNeuro.API.IAM.Interfaces.REST.Resources;

/// <summary>
/// Resource for user sign-up requests.
/// </summary>
/// <param name="Username">The username for sign-up.</param>
/// <param name="Password">The password for sign-up.</param>
public record SignUpResource(
    string Username,
    string Password,
    string Role,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime BirthDate,
    string Street,
    string Number,
    string City,
    string Country,
    string LicenseNumber,
    string PostalCode,
    string Specialties
    );