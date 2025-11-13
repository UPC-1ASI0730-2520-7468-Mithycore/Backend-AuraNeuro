namespace Backend_AuraNeuro.API.Appointments.Domain.Model.Aggregates;

using Backend_AuraNeuro.API.Appointments.Domain.Model.Commands;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

public class Appointment : IEntityWithCreatedUpdatedDate
{
    public long Id { get; private set; }
    public long NeurologistId { get; private set; }
    public long PatientId { get; private set; }
    public DateTime Date { get; private set; }

    public string Status { get; private set; } = "Scheduled";
    public string Notes { get; private set; }

    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }

    protected Appointment() {}

    public Appointment(CreateAppointmentCommand command)
    {
        NeurologistId = command.NeurologistId;
        PatientId = command.PatientId;
        Date = command.Date;
        Notes = command.Notes;
    }
}