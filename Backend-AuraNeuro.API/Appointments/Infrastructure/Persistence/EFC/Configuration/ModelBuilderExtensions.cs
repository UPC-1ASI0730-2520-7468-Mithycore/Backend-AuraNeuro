using Backend_AuraNeuro.API.Appointments.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend_AuraNeuro.API.Appointments.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAppointmentsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Appointment>(ConfigureAppointment);
    }

    private static void ConfigureAppointment(EntityTypeBuilder<Appointment> entity)
    {
        // Nombre FINAL de la tabla
        entity.ToTable("appointments");

        // Primary Key
        entity.HasKey(a => a.Id);
        entity.Property(a => a.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        // Required fields
        entity.Property(a => a.NeurologistId).IsRequired();
        entity.Property(a => a.PatientId).IsRequired();
        entity.Property(a => a.Date).IsRequired();
        entity.Property(a => a.Status).IsRequired().HasMaxLength(50);
        entity.Property(a => a.Notes).HasMaxLength(500);

        // Created/Updated (audit)
    }
}