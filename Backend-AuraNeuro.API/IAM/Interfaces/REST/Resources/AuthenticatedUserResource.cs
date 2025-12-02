namespace Backend_AuraNeuro.API.IAM.Interfaces.REST.Resources;

/// <summary>
/// Resource representing an authenticated user with a JWT token.
/// </summary>
/// <param name="Id">The unique identifier of the user.</param>
/// <param name="Username">The username of the user.</param>
/// <param name="Token">The JWT token for authentication.</param>
public record AuthenticatedUserResource(long Id, string Token);