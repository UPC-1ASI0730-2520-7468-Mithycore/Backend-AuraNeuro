namespace Backend_AuraNeuro.API.Appointments.Interfaces.REST.Resources;

public record CreateAppointmentResource(
    long NeurologistId,
    long PatientId,
    DateTime Date,
    string Notes
);