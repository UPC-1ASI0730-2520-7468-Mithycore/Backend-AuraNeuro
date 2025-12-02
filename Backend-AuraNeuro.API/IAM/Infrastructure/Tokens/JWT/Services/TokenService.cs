using System.Security.Claims;
using System.Text;
using Backend_AuraNeuro.API.IAM.Application.Internal.OutboundServices;
using Backend_AuraNeuro.API.IAM.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Backend_AuraNeuro.API.IAM.Infrastructure.Tokens.JWT.Services;

/// <summary>
///     JWT token generation and validation implementation.
/// </summary>
public class TokenService(IOptions<TokenSettings> tokenSettings) : ITokenService
{
    private readonly TokenSettings _tokenSettings = tokenSettings.Value;

    /// <summary>
    ///     Generates a signed JWT token for the provided user.
    /// </summary>
    /// <param name="user">The user for whom to create the token.</param>
    /// <returns>The JWT token string.</returns>
    public string GenerateTokenPatient(Patient.Domain.Model.Aggregates.Patient user)
    {
        var secret = _tokenSettings.Secret;
        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name.FullName),
                new Claim(ClaimTypes.Role, "Patient"),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JsonWebTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return token;
    }
    
    public string GenerateTokenNeurologist(Neurologist.Domain.Model.Aggregates.Neurologist user)
    {
        var secret = _tokenSettings.Secret;
        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, "Neurologist"),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JsonWebTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return token;
    }

    /// <summary>
    ///     Validates the provided JWT token and returns the embedded user id when valid.
    /// </summary>
    /// <param name="token">The JWT token to validate.</param>
    /// <returns>The user id extracted from the token when valid; otherwise <c>null</c>.</returns>
    public async Task<(int userId, string roleClaim)?> ValidateToken(string token)
    {
        // If token is null or empty return null
        if (string.IsNullOrEmpty(token))
            return null;

        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);

        try
        {
            var tokenValidationResult = await tokenHandler.ValidateTokenAsync(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                });

            // Si la validación falla, devolvemos null
            if (!tokenValidationResult.IsValid || tokenValidationResult.SecurityToken is not JsonWebToken jwtToken)
                return null;

            // Sacamos los valores de los claims como string primero
            var userIdClaim = jwtToken.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.Sid)?.Value;

            var roleClaim = jwtToken.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;

            // Si falta alguno de los claims, token inválido
            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
                return null;

            // Intentamos parsear el userId
            if (!int.TryParse(userIdClaim, out var userId))
                return null;

            // Todo bien: devolvemos tupla (userId, role)
            return (userId, roleClaim);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }   
}