using Backend_AuraNeuro.API.Neurologist.Domain.Model.Commands;
using NeurologistProfile = Backend_AuraNeuro.API.Neurologist.Domain.Model.Aggregates.Neurologist;

namespace Backend_AuraNeuro.API.Neurologist.Domain.Services.Command;

/// <summary>
/// Command (write) services for neurologists.
/// </summary>
public interface INeurologistCommandService
{
    Task<NeurologistProfile?> Handle(CreateNeurologistCommand command);

    Task<NeurologistProfile?> Handle(UpdateNeurologistProfileCommand command);

    Task<bool> Handle(DeactivateNeurologistCommand command);
}