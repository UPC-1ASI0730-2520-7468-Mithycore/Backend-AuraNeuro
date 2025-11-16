namespace Backend_AuraNeuro.API.Patient.Domain.Command;

public record AssignNeurologistToPatientCommand(long PatientId, long NeurologistId);