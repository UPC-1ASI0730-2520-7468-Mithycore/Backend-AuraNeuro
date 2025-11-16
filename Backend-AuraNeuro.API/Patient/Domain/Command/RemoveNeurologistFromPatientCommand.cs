namespace Backend_AuraNeuro.API.Patient.Domain.Command;

public record RemoveNeurologistFromPatientCommand(long PatientId, long NeurologistId);