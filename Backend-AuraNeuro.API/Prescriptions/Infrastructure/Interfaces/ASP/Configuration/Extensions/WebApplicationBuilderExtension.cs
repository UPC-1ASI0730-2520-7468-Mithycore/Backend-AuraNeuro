// Summary: Registers prescription services and repository into DI container.
using Backend_AuraNeuro.API.Prescriptions.Application.Internal.CommandServices;
using Backend_AuraNeuro.API.Prescriptions.Application.Internal.QueryServices;
using Backend_AuraNeuro.API.Prescriptions.Domain.Model.Repositories;
using Backend_AuraNeuro.API.Prescriptions.Domain.Services;
using Backend_AuraNeuro.API.Prescriptions.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Backend_AuraNeuro.API.Prescriptions.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPrescriptionsContextServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        builder.Services.AddScoped<IPrescriptionCommandService, PrescriptionCommandService>();
        builder.Services.AddScoped<IPrescriptionQueryService, PrescriptionQueryService>();
    }
}