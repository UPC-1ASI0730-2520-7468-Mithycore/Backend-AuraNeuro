using Backend_AuraNeuro.API.Appointments.Application.Internal.CommandServices;
using Backend_AuraNeuro.API.Appointments.Application.Internal.QueryServices;
using Backend_AuraNeuro.API.Appointments.Domain.Repositories;
using Backend_AuraNeuro.API.Appointments.Domain.Services.Command;
using Backend_AuraNeuro.API.Appointments.Domain.Services.Queries;
using Backend_AuraNeuro.API.Appointments.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Backend_AuraNeuro.API.Appointments.Infrastructure.ASP.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddAppointmentsContextServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        builder.Services.AddScoped<IAppointmentCommandService, AppointmentCommandService>();
        builder.Services.AddScoped<IAppointmentQueryService, AppointmentQueryService>();
    }
}