using System.Net.Mime;
using Backend_AuraNeuro.API.IAM.Application.Internal.OutboundServices;
using Backend_AuraNeuro.API.IAM.Domain.Services;
using Backend_AuraNeuro.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Backend_AuraNeuro.API.IAM.Interfaces.REST.Resources;
using Backend_AuraNeuro.API.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend_AuraNeuro.API.IAM.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication endpoints")]
public class AuthenticationController(IUserCommandService userCommandService, ITokenService tokenService) : ControllerBase
{
    /// <summary>
    ///     Authenticate a user using username and password.
    /// </summary>
    /// <param name="signInResource">The sign-in resource containing credentials.</param>
    /// <returns>
    ///     Returns an <see cref="IActionResult" /> that contains an <see cref="AuthenticatedUserResource" /> with a JWT
    ///     token when authentication succeeds.
    /// </returns>
    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Sign in",
        Description = "Sign in a user",
        OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was authenticated", typeof(AuthenticatedUserResource))]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
        var authenticatedUser = await userCommandService.Handle(signInCommand);
        var resource =
            AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(authenticatedUser.id,
                authenticatedUser.token);
        Response.Cookies.Append(
            "AuthToken",                          // nombre de la cookie
            authenticatedUser.token,              // el JWT
            new CookieOptions
            {
                HttpOnly = true,                  // evita acceso desde JS → más seguro
                Secure = false,                    // HTTPS obligatorio (en producción)
                SameSite = SameSiteMode.Lax,         // necesario si tu frontend está en otro dominio
                Expires = DateTime.UtcNow.AddDays(7)
            }
        );

        
        return Ok(new {id = authenticatedUser.id});
    }

    /// <summary>
    ///     Create a new user account (sign up).
    /// </summary>
    /// <param name="signUpResource">The sign-up resource containing the new user's credentials.</param>
    /// <returns>Returns an <see cref="IActionResult" /> that indicates the operation result.</returns>
    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Sign-up",
        Description = "Sign up a new user",
        OperationId = "SignUp")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was created successfully")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource signUpResource)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(signUpResource);
        var authenticatedUser = await userCommandService.Handle(signUpCommand);
        var resource =
            AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(authenticatedUser.id,
                authenticatedUser.token);
        Response.Cookies.Append(
            "AuthToken",                          // nombre de la cookie
            authenticatedUser.token,              // el JWT
            new CookieOptions
            {
                HttpOnly = true,                  // evita acceso desde JS → más seguro
                Secure = false,                    // HTTPS obligatorio (en producción)
                SameSite = SameSiteMode.Lax,         // necesario si tu frontend está en otro dominio
                Expires = DateTime.UtcNow.AddDays(7)
            }
        );

        
        return Ok(new {id = authenticatedUser.id});
    }
    
    [HttpGet("validate")]
    [AllowAnonymous]
    public async Task<IActionResult> Validate()
    {
        // Leer el token desde la cookie
        var token = Request.Cookies["AuthToken"];

        if (string.IsNullOrEmpty(token))
            return Unauthorized(new { message = "Missing token cookie" });

        // Validar token y obtener userId
        var user = await tokenService.ValidateToken(token);
        
        if (user == null)
            return Unauthorized(new { message = "Invalid token" });
        
        return Ok(new
        {
            userId = user.Value.userId,
            role = user.Value.roleClaim
        });
    }
}