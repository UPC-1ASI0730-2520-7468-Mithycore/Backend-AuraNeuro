namespace Backend_AuraNeuro.API.Appointment.Domain.Model.Commands;

public record CreateAppointmentCommand(
    long NeurologistId,
    long PatientId,
    DateTime Date,
    string Notes
);