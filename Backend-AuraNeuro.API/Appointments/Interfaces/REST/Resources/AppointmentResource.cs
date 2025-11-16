namespace Backend_AuraNeuro.API.Appointments.Interfaces.REST.Resources;

public record AppointmentResource(
    long Id,
    long NeurologistId,
    long PatientId,
    DateTime Date,
    string Status,
    string Notes
);