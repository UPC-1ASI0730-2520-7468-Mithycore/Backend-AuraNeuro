using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeurologistProfile = Backend_AuraNeuro.API.Neurologist.Domain.Model.Aggregates.Neurologist;

namespace Backend_AuraNeuro.API.Neurologist.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
/// EF Core configuration for Neurologist aggregate.
/// </summary>
public static class ModelBuilderExtensions
{
    public static void ApplyNeurologistsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<NeurologistProfile>(ConfigureNeurologist);
    }

    private static void ConfigureNeurologist(EntityTypeBuilder<NeurologistProfile> entity)
    {
        entity.ToTable("neurologist_profiles");

        entity.HasKey(n => n.Id);

        entity.Property(n => n.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        entity.Property(n => n.UserId)
            .IsRequired();

        entity.Property(n => n.Specialties)
            .IsRequired(false);

        entity.Property(n => n.VerificationStatus)
            .IsRequired();

        entity.Property(n => n.IsActive)
            .IsRequired();

        // Audit columns (from partial)
        entity.Property(n => n.CreatedDate)
            .HasColumnName("created_at");

        entity.Property(n => n.UpdatedDate)
            .HasColumnName("updated_at");

        // ===== Owned types =====

        entity.OwnsOne(n => n.Name, name =>
        {
            name.Property(p => p.FirstName).HasColumnName("first_name");
            name.Property(p => p.LastName).HasColumnName("last_name");
        });

        entity.OwnsOne(n => n.Email, email =>
        {
            email.Property(e => e.Address).HasColumnName("email");
        });

        entity.OwnsOne(n => n.Phone, phone =>
        {
            phone.Property(p => p.Number).HasColumnName("phone");
        });

        entity.OwnsOne(n => n.Address, address =>
        {
            address.Property(a => a.Street).HasColumnName("street");
            address.Property(a => a.Number).HasColumnName("number");
            address.Property(a => a.City).HasColumnName("city");
            address.Property(a => a.PostalCode).HasColumnName("postal_code");
            address.Property(a => a.Country).HasColumnName("country");
        });

        entity.OwnsOne(n => n.LicenseNumber, license =>
        {
            license.Property(l => l.Value).HasColumnName("license_number");
        });

        // Indexes
        entity.HasIndex(n => n.UserId).IsUnique();
        //entity.HasIndex("license_number").IsUnique();
    }
}
