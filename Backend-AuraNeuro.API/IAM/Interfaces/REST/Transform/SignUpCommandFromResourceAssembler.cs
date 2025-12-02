using Backend_AuraNeuro.API.IAM.Domain.Model.Commands;
using Backend_AuraNeuro.API.IAM.Interfaces.REST.Resources;

namespace Backend_AuraNeuro.API.IAM.Interfaces.REST.Transform;

/// <summary>
/// Assembler for converting SignUpResource DTOs to SignUpCommand objects.
/// </summary>
public static class SignUpCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a SignUpResource to a SignUpCommand.
    /// </summary>
    /// <param name="resource">The SignUpResource containing sign-up data.</param>
    /// <returns>A SignUpCommand for user registration.</returns>
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(
            resource.Username,
            resource.Password,
            resource.Role,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.PhoneNumber,
            resource.BirthDate,
            resource.City,
            resource.Number,
            resource.City,
            resource.Country,
            resource.LicenseNumber,
            resource.PostalCode,
            resource.Specialties
            );
    }
}