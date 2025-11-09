using Backend_AuraNeuro.API.IAM.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Aggregates;
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
            if (!string.IsNullOrWhiteSpace(currentTableName))
            {
            }

            var desiredTableName = entityType.ClrType.Name
                .Pluralize() // Patient -> Patients
                .Underscore() // PatientMetrics -> patient_metrics
                .ToLowerInvariant();

            entityType.SetTableName(desiredTableName);
        }

        modelBuilder.Entity<User>().HasKey(f => f.Id);
        modelBuilder.Entity<User>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<User>().Property(f => f.Names).IsRequired();
        modelBuilder.Entity<User>(entity =>
        {
            entity.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.DirectionEmail)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasMaxLength(150);
            });
        });

        modelBuilder.Entity<User>().Property(f => f.PasswordHash).IsRequired();
        modelBuilder.Entity<User>(entity =>
        {
            entity.OwnsOne(u => u.Role, role =>
            {
                role.Property(r => r.UserRole)
                    .HasColumnName("Role")
                    .IsRequired()
                    .HasMaxLength(50);
            });
        });
        //Bounded context NeurologicalHealth
        modelBuilder.Entity<NeuroAssessment>().HasKey(n => n.Id);
        modelBuilder.Entity<NeuroAssessment>().Property(n => n.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<NeuroAssessment>().Property(n => n.PatientId);
        modelBuilder.Entity<NeuroAssessment>().Property(n => n.GaitScore);
        modelBuilder.Entity<NeuroAssessment>().Property(n => n.BalanceScore);
        modelBuilder.Entity<NeuroAssessment>().Property(n => n.ReflexScore);
        modelBuilder.Entity<NeuroAssessment>().Property(n => n.CognitionScore);
        modelBuilder.Entity<NeuroAssessment>().Property(n => n.MemoryScore);
        modelBuilder.Entity<NeuroAssessment>().Property(n => n.SpeechScore);
        modelBuilder.Entity<NeuroAssessment>().Property(n => n.TremorScore);
        modelBuilder.Entity<NeuroAssessment>().Property(n => n.StrengthScore);
        modelBuilder.Entity<NeuroAssessment>().Property(n => n.CoordinationScore);
        modelBuilder.Entity<NeuroAssessment>().Property(n => n.SensoryScore);
        modelBuilder.Entity<NeuroAssessment>().Property(n => n.EegSummary);
        modelBuilder.Entity<NeuroAssessment>().Property(n => n.NeurologistNotes);
        modelBuilder.Entity<NeuroAssessment>().Property(n => n.IsFlagged);
        modelBuilder.Entity<NeuroAssessment>().Property(n => n.AlertLevel);
        
    }
}