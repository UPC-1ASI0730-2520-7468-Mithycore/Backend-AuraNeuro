using Backend_AuraNeuro.API.IAM.Domain.Model.Commands;
using Backend_AuraNeuro.API.IAM.Domain.Model.Queries;
using Backend_AuraNeuro.API.IAM.Domain.Services;
using Backend_AuraNeuro.API.IAM.Interfaces.ACL;

namespace Backend_AuraNeuro.API.IAM.Application.ACL.Services;

/// <summary>
/// Facade for the IAM context, providing access to user-related operations.
/// </summary>
public class IamContextFacade(IUserCommandService userCommandService, IUserQueryService userQueryService)
    : IIamContextFacade
{
    /// <summary>
    /// Creates a new user with the specified username and password.
    /// </summary>
    /// <param name="username">The username for the new user.</param>
    /// <param name="password">The password for the new user.</param>
    /// <returns>The ID of the created user, or 0 if creation failed.</returns>
    public async Task<long> CreateUser(
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
        string Specialties)
    {
        var signUpCommand = new SignUpCommand(
            Username, Password, Role,  FirstName, LastName,  Email, PhoneNumber, BirthDate, Street, Number,City, Country, LicenseNumber, PostalCode, Specialties
            );
        await userCommandService.Handle(signUpCommand);
        var getUserByUsernameQuery = new GetUserByUsernameQuery(Username);
        var result = await userQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? 0;
    }

    /// <summary>
    /// Fetches the user ID by username.
    /// </summary>
    /// <param name="username">The username to search for.</param>
    /// <returns>The user ID if found, otherwise 0.</returns>
    public async Task<long> FetchUserIdByUsername(string username)
    {
        var getUserByUsernameQuery = new GetUserByUsernameQuery(username);
        var result = await userQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? 0;
    }

    /// <summary>
    /// Fetches the username by user ID.
    /// </summary>
    /// <param name="userId">The user ID to search for.</param>
    /// <returns>The username if found, otherwise an empty string.</returns>
    public async Task<string> FetchUsernameByUserId(long userId)
    {
        var getUserByIdQuery = new GetUserByIdQuery(userId);
        var result = await userQueryService.Handle(getUserByIdQuery);
        return result?.Username ?? string.Empty;
    }
}