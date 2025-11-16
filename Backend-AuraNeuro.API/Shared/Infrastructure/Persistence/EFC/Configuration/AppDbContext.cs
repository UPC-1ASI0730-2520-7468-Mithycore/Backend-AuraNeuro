using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.Appointments.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.Appointments.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Humanizer;
using Microsoft.EntityFrameworkCore;

// Importamos configuraciones de los bounded contexts
using Backend_AuraNeuro.API.Patient.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<NeuroAssessment> NeuroAssessments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // ------------------------------------------------
        // REGISTER ALL ENTITY CONFIGURATIONS FIRST
        // ------------------------------------------------
        modelBuilder.ApplyAppointmentsConfiguration();

        modelBuilder.Entity<NeuroAssessment>(entity =>
        {
            entity.HasKey(n => n.Id);
            entity.Property(n => n.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(n => n.PatientId).IsRequired();
            entity.Property(n => n.GaitScore).IsRequired();
            entity.Property(n => n.BalanceScore).IsRequired();
            entity.Property(n => n.ReflexScore).IsRequired();
            entity.Property(n => n.CognitionScore).IsRequired();
            entity.Property(n => n.MemoryScore).IsRequired();
            entity.Property(n => n.SpeechScore).IsRequired();
            entity.Property(n => n.TremorScore).IsRequired();
            entity.Property(n => n.StrengthScore).IsRequired();
            entity.Property(n => n.CoordinationScore).IsRequired();
            entity.Property(n => n.SensoryScore).IsRequired();

            entity.Property(n => n.EegSummary).HasMaxLength(500);
            entity.Property(n => n.NeurologistNotes).HasMaxLength(500);

            entity.Property(n => n.IsFlagged).IsRequired();
            entity.Property(n => n.AlertLevel).IsRequired();
        });

        // ------------------------------------------------
        // APPLY SNAKE_CASE + PLURALIZATION AFTER CONFIGS
        // ------------------------------------------------

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType == typeof(Appointment))
                continue;

            var desiredTableName = entityType.ClrType.Name
                .Pluralize()
                .Underscore()
                .ToLowerInvariant();

            entityType.SetTableName(desiredTableName);
        }

        // ========= Patient Bounded Context (lo nuevo) =========
        modelBuilder.ApplyConfiguration(new PatientNeurologistConfiguration());
        modelBuilder.ApplyPatientsConfiguration();
    }
}
