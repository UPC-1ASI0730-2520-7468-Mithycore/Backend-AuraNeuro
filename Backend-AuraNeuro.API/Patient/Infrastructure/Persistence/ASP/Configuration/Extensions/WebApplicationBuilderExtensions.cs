using Backend_AuraNeuro.API.Patient.Application.Internal.CommandServices;
using Backend_AuraNeuro.API.Patient.Application.Internal.QueryServices;
using Backend_AuraNeuro.API.Patient.Domain.Repositories;
using Backend_AuraNeuro.API.Patient.Domain.Services.Command;
using Backend_AuraNeuro.API.Patient.Domain.Services.Queries;
using Backend_AuraNeuro.API.Patient.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Backend_AuraNeuro.API.Patient.Infrastructure.Persistence.ASP.Configuration.Extensions
{
    /// <summary>
    /// Registers Patient bounded context services.
    /// </summary>
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPatientsContextServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IPatientCommandService, PatientCommandService>();
            builder.Services.AddScoped<IPatientQueryService, PatientQueryService>();
        }
    }
}