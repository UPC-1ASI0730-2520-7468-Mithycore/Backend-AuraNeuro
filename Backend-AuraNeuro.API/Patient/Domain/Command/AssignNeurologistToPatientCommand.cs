namespace Backend_AuraNeuro.API.Patient.Domain.Command;

public record AssignNeurologistToPatientCommand(int PatientId, long NeurologistId);