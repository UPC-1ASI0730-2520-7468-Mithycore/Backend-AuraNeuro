using Backend_AuraNeuro.API.IAM.Application.Internal.CommandServices;
using Backend_AuraNeuro.API.IAM.Domain.Repositories;
using Backend_AuraNeuro.API.IAM.Domain.Services;
using Backend_AuraNeuro.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using Backend_AuraNeuro.API.IAM.Infrastructure.Security;
using Backend_AuraNeuro.API.NeurologicalHealth.Applications.Internal.CommandServices;
using Backend_AuraNeuro.API.NeurologicalHealth.Applications.Internal.QueryServices;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Repositories;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Services.Command;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Services.Queries;
using Backend_AuraNeuro.API.NeurologicalHealth.Infrastructure.Persistence.EFC.Repositories;
using Backend_AuraNeuro.API.Shared.Domain.Repositories;
using Backend_AuraNeuro.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC;
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// change names tha controller to kebab-case
builder.Services.AddControllers(options =>
{
    //use kebab-case for names of the controllers and actions
    options.Conventions.Add(new RouteTokenTransformerConvention(
        new KebabCaseParameterTransformer()
    ));
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

//Add Database Connection
if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        if(connectionString is null) throw new Exception("Database connection string not found.");
        options.UseMySQL(connectionString, mySqlOptions =>
            {
                mySqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
            })
            .UseSnakeCaseNamingConvention()
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    });

// Configure Dependency Injection
// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Iam Bounded Context
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IJwtService, JwtService>();

//NeurologicalHealth Bounded Context
builder.Services.AddScoped<INeurologicalHealthRepository, NeurologicalHealthRepository>();
builder.Services.AddScoped<INeuroAssessmentCommandService, NeuroAssessmentCommandService>();
builder.Services.AddScoped<INeuroAssessmentQueryService, NeuroAssessmentQueryService>();

var app = builder.Build();

// Verify Database Creation
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();