using Backend_AuraNeuro.API.Neurologist.Application.Internal.CommandServices;
using Backend_AuraNeuro.API.Neurologist.Application.Internal.QueryServices;
using Backend_AuraNeuro.API.Neurologist.Domain.Repositories;
using Backend_AuraNeuro.API.Neurologist.Domain.Services.Command;
using Backend_AuraNeuro.API.Neurologist.Domain.Services.Queries;
using Backend_AuraNeuro.API.Neurologist.Infrastructure.Persistence.EFC.Repositories;

namespace Backend_AuraNeuro.API.Neurologist.Infrastructure.Persistence.ASP.Configuration.Extensions
{
    /// <summary>
    /// Registers Neurologists bounded context services.
    /// </summary>
    public static class WebApplicationBuilderExtensions
    {
        public static void AddNeurologistsContextServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<INeurologistRepository, NeurologistRepository>();
            builder.Services.AddScoped<INeurologistCommandService, NeurologistCommandService>();
            builder.Services.AddScoped<INeurologistQueryService, NeurologistQueryService>();
        }
    }
}