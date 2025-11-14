using Backend_AuraNeuro.API.Neurologist.Domain.Queries;
using NeurologistProfile = Backend_AuraNeuro.API.Neurologist.Domain.Model.Aggregates.Neurologist;

namespace Backend_AuraNeuro.API.Neurologist.Domain.Services.Queries;

/// <summary>
/// Query (read) services for neurologists.
/// </summary>
public interface INeurologistQueryService
{
    Task<IEnumerable<NeurologistProfile>> Handle(GetAllNeurologistsQuery query);

    Task<NeurologistProfile?> Handle(GetNeurologistByIdQuery query);
}