using Backend_AuraNeuro.API.Patient.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend_AuraNeuro.API.Patient.Infrastructure.Persistence.EFC.Configuration.Extensions;

public class PatientNeurologistConfiguration : IEntityTypeConfiguration<PatientNeurologist>
{
    public void Configure(EntityTypeBuilder<PatientNeurologist> entity)
    {
        entity.ToTable("patient_neurologists");

        entity.HasKey(r => r.Id);

        entity.Property(r => r.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        entity.Property(r => r.PatientId)
            .IsRequired();

        entity.Property(r => r.NeurologistId)
            .IsRequired();

        entity.Property(r => r.IsActive)
            .HasDefaultValue(true);

        // OPTIONAL: Prevent duplicate associations
        entity.HasIndex(r => new { r.PatientId, r.NeurologistId }).IsUnique();
    }
}