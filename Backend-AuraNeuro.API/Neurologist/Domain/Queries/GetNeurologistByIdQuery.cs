namespace Backend_AuraNeuro.API.Neurologist.Domain.Queries;

/// <summary>
/// Query to get a neurologist by their Id.
//
public record GetNeurologistByIdQuery(long NeurologistId);