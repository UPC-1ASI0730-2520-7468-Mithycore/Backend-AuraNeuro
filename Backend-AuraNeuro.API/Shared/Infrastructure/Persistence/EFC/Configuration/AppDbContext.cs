using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var currentTableName = entityType.GetTableName();
            if (!string.IsNullOrWhiteSpace(currentTableName)) { }
            
            var desiredTableName = entityType.ClrType.Name
                .Pluralize()      // Patient -> Patients
                .Underscore()     // PatientMetrics -> patient_metrics
                .ToLowerInvariant();

            entityType.SetTableName(desiredTableName);
        }
        
        
    }
}