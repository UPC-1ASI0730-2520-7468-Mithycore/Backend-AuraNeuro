using Backend_AuraNeuro.API.Appointment.Domain.Model.Commands;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace Backend_AuraNeuro.API.Appointment.Domain.Model.Aggregates;

public class Appointment : IEntityWithCreatedUpdatedDate
{
    public long Id { get; private set; }

    public long NeurologistId { get; private set; }
    public long PatientId { get; private set; }

    public DateTime Date { get; private set; }
    public string Status { get; private set; }
    public string Notes { get; private set; }

    // Audit fields
    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }

    protected Appointment() { }

    public Appointment(CreateAppointmentCommand command)
    {
        NeurologistId = command.NeurologistId;
        PatientId = command.PatientId;
        Date = command.Date;
        Status = "Scheduled";
        Notes = command.Notes;
    }

    public void UpdateStatus(string newStatus)
    {
        Status = newStatus;
    }

    public void UpdateNotes(string notes)
    {
        Notes = notes;
    }
}