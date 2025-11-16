namespace Backend_AuraNeuro.API.Patient.Domain.Command;

public record RemoveNeurologistFromPatientCommand(int PatientId, long NeurologistId);