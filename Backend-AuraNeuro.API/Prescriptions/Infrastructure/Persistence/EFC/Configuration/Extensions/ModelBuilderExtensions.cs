using System.Collections.Generic;
using System.Text.Json;
using Backend_AuraNeuro.API.Prescriptions.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.Prescriptions.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Backend_AuraNeuro.API.Prescriptions.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyPrescriptionsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Prescription>(entity =>
        {
            // Tabla
            entity.ToTable("prescriptions");

            // PK
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasColumnName("id");

            entity.Property(p => p.PatientId)
                .IsRequired();

            entity.Property(p => p.NeurologistId)
                .IsRequired();

            entity.Property(p => p.IssuedAt)
                .IsRequired();

            entity.Property(p => p.SignatureHash)
                .IsRequired();

            entity.Property(p => p.Revoked)
                .HasDefaultValue(false);
            

            // JSON column for Medicines
            entity.Property(p => p.Medicines)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<MedicineItem>>(v, (JsonSerializerOptions?)null)
                         ?? new List<MedicineItem>());
        });
    }
}