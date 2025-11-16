using Backend_AuraNeuro.API.Patient.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend_AuraNeuro.API.Patient.Infrastructure.Persistence.EFC.Configuration.Extensions
{
    /// <summary>
    /// EF Core configuration for Patient aggregate.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        public static void ApplyPatientsConfiguration(this ModelBuilder builder)
        {
            builder.Entity<Domain.Model.Aggregates.Patient>(ConfigurePatient);
        }

        private static void ConfigurePatient(EntityTypeBuilder<Domain.Model.Aggregates.Patient> entity)
        {
            entity.ToTable("patients");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(p => p.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            entity.Property(p => p.IsActive)
                .IsRequired();

            // ========== OWNED TYPES ==========

            // Name (PersonName)
            entity.OwnsOne(p => p.Name, name =>
            {
                name.Property(n => n.FirstName).HasColumnName("first_name");
                name.Property(n => n.LastName).HasColumnName("last_name");
            });

            // Email
            entity.OwnsOne(p => p.Email, email =>
            {
                email.Property(e => e.Address).HasColumnName("email");
            });

            // Phone
            entity.OwnsOne(p => p.PhoneNumber, phone =>
            {
                phone.Property(pn => pn.Number).HasColumnName("phone_number");
            });

            // BirthDate
            entity.OwnsOne(p => p.BirthDate, birth =>
            {
                birth.Property(b => b.Date).HasColumnName("birth_date");
            });

            // Address (optional)
            entity.OwnsOne(p => p.Address, address =>
            {
                address.Property(a => a.Street).HasColumnName("street");
                address.Property(a => a.City).HasColumnName("city");
                address.Property(a => a.Country).HasColumnName("country");
            });

            // ========= INDEXES =========

            entity.HasIndex(p => p.UserId).IsUnique();
            entity.OwnsOne(p => p.Email, email =>
            {
                email.Property(e => e.Address).HasColumnName("email");
                email.HasIndex(e => e.Address);
            });
        }
    }
}
