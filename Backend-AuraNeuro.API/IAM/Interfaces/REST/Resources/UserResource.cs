namespace Backend_AuraNeuro.API.IAM.Interfaces.REST.Resources;

/// <summary>
/// Resource representing a user for REST API responses.
/// </summary>
/// <param name="Id">The unique identifier of the user.</param>
/// <param name="Username">The username of the user.</param>
public record UserResource(long Id, string Username);