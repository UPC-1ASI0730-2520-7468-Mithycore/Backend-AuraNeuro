using Backend_AuraNeuro.API.IAM.Application.ACL.Services;
using Backend_AuraNeuro.API.IAM.Application.Internal.CommandServices;
using Backend_AuraNeuro.API.IAM.Application.Internal.OutboundServices;
using Backend_AuraNeuro.API.IAM.Application.Internal.QueryServices;
using Backend_AuraNeuro.API.IAM.Domain.Repositories;
using Backend_AuraNeuro.API.IAM.Domain.Services;
using Backend_AuraNeuro.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using Backend_AuraNeuro.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using Backend_AuraNeuro.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using Backend_AuraNeuro.API.IAM.Infrastructure.Tokens.JWT.Services;
using Backend_AuraNeuro.API.IAM.Interfaces.ACL;

namespace Backend_AuraNeuro.API.IAM.Infrastructure.Interfaces.ASP.Configuration.Extensions;

/// <summary>
///     Extension methods for configuring IAM context services in a WebApplicationBuilder.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    ///     Adds the IAM context services to the WebApplicationBuilder.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder to configure.</param>
    public static void AddIamContextServices(this WebApplicationBuilder builder)
    {
        // IAM Bounded Context Injection Configuration

        // TokenSettings Configuration

        builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserCommandService, UserCommandService>();
        builder.Services.AddScoped<IUserQueryService, UserQueryService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IHashingService, HashingService>();
        builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();
    }
}