namespace Backend_AuraNeuro.API.Appointments.Infrastructure.Persistence.EFC.Configuration;

using Backend_AuraNeuro.API.Appointments.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class ModelBuilderExtensions
{
    public static void ApplyAppointmentsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Appointment>(ConfigureAppointment);
    }

    private static void ConfigureAppointment(EntityTypeBuilder<Appointment> entity)
    {
        entity.ToTable("appointments");

        entity.HasKey(a => a.Id);

        entity.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        entity.Property(a => a.NeurologistId).IsRequired();
        entity.Property(a => a.PatientId).IsRequired();
        entity.Property(a => a.Date).IsRequired();
        entity.Property(a => a.Status).IsRequired();
        entity.Property(a => a.Notes).IsRequired(false);

        entity.Property(a => a.CreatedDate).HasColumnName("created_at");
        entity.Property(a => a.UpdatedDate).HasColumnName("updated_at");
    }
}