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
        // ---------------------------------------------------------
        // Forzar nombres de tablas: plural + snake_case (compatible
        // con cualquier proveedor y con EFCore.NamingConventions)
        // ---------------------------------------------------------
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            // Evita cambiar tablas ya configuradas explícitamente:
            // Si el desarrollador puso [Table("...")] o SetTableName antes,
            // puedes omitir (opcional): check entityType.GetTableName()
            var currentTableName = entityType.GetTableName();
            if (!string.IsNullOrWhiteSpace(currentTableName))
            {
                // Si el nombre ya está en snake_case y plural, opcionalmente saltar.
                // En la práctica puedes forzar siempre para consistencia:
            }

            // Construye nombre: plural, underscore, lowercase
            var desiredTableName = entityType.ClrType.Name
                .Pluralize()      // Patient -> Patients
                .Underscore()     // PatientMetrics -> patient_metrics
                .ToLowerInvariant();

            entityType.SetTableName(desiredTableName);
        }

        // NOTA: No forzamos nombres de columnas aquí porque
        // UseSnakeCaseNamingConvention() (EFCore.NamingConventions) ya las maneja.
        // Si en tu caso las columnas no quedan como quieres, puedes activar
        // el bloque de columnas (lo dejo comentado):

        /*
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var prop in entityType.GetProperties())
            {
                var columnName = prop.Name.Underscore().ToLowerInvariant();
                prop.SetColumnName(columnName);
            }
        }
        */
    }
}