using Backend_AuraNeuro.API.NeurologicalHealth.Applications.Internal.CommandServices;
using Backend_AuraNeuro.API.NeurologicalHealth.Applications.Internal.QueryServices;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Repositories;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Services.Command;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Services.Queries;
using Backend_AuraNeuro.API.NeurologicalHealth.Infrastructure.Persistence.EFC.Repositories;


using Backend_AuraNeuro.API.Patient.Application.Internal.CommandServices;
using Backend_AuraNeuro.API.Patient.Application.Internal.QueryServices;
using Backend_AuraNeuro.API.Patient.Domain.Repositories;
using Backend_AuraNeuro.API.Patient.Domain.Services.Command;
using Backend_AuraNeuro.API.Patient.Domain.Services.Queries;
using Backend_AuraNeuro.API.Patient.Infrastructure.Persistence.EFC.Repositories;


using Backend_AuraNeuro.API.Appointments.Domain.Repositories;
using Backend_AuraNeuro.API.Appointments.Domain.Services.Command;
using Backend_AuraNeuro.API.Appointments.Domain.Services.Queries;
using Backend_AuraNeuro.API.Appointments.Application.Internal.CommandServices;
using Backend_AuraNeuro.API.Appointments.Application.Internal.QueryServices;
using Backend_AuraNeuro.API.Appointments.Infrastructure.Persistence.EFC.Repositories;

// Shared
using Backend_AuraNeuro.API.Shared.Domain.Repositories;
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC;               // ✔ UnitOfWork
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC.Configuration;  // ✔ AppDbContext

using Backend_AuraNeuro.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

//bounded context Prescription
using Backend_AuraNeuro.API.Prescriptions.Infrastructure.Interfaces.ASP.Configuration.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

// change names tha controller to kebab-case
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseParameterTransformer()));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

// -------------------------
// DATABASE CONNECTION
// -------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (connectionString is null)
        throw new Exception("Database connection string not found.");

    options.UseMySQL(connectionString, mySqlOptions =>
    {
        mySqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
    })
    .UseSnakeCaseNamingConvention()
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors();
});

// -------------------------
// SHARED
// -------------------------
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// -------------------------
// NEUROLOGICAL HEALTH
// -------------------------
builder.Services.AddScoped<INeurologicalHealthRepository, NeurologicalHealthRepository>();
builder.Services.AddScoped<INeuroAssessmentCommandService, NeuroAssessmentCommandService>();
builder.Services.AddScoped<INeuroAssessmentQueryService, NeuroAssessmentQueryService>();

//Prescription Bounded Context
builder.AddPrescriptionsContextServices();


// Patient Bounded Context
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientCommandService, PatientCommandService>();
builder.Services.AddScoped<IPatientQueryService, PatientQueryService>();

// -------------------------
// APPOINTMENTS
// -------------------------
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentCommandService, AppointmentCommandService>();
builder.Services.AddScoped<IAppointmentQueryService, AppointmentQueryService>();


var app = builder.Build();

// -------------------------
// ENSURE DATABASE CREATED
// -------------------------
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// -------------------------
// SWAGGER UI
// -------------------------
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();
app.Run();
