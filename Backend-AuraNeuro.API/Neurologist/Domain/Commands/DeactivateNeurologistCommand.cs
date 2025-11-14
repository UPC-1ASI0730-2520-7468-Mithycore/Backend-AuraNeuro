namespace Backend_AuraNeuro.API.Neurologist.Domain.Commands;

/// <summary>
/// Command to perform a soft delete (deactivate profile).
/// </summary>
public record DeactivateNeurologistCommand(long NeurologistId);