namespace Backend_AuraNeuro.API.IAM.Interfaces.REST.Resources;

/// <summary>
/// Resource for user sign-in requests.
/// </summary>
/// <param name="Username">The username for sign-in.</param>
/// <param name="Password">The password for sign-in.</param>
public record SignInResource(string Username, string Password, string Role);