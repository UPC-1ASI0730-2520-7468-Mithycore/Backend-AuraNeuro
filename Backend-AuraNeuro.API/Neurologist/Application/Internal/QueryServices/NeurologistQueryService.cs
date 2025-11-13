using Backend_AuraNeuro.API.Neurologist.Domain.Model.Queries;
using Backend_AuraNeuro.API.Neurologist.Domain.Repositories;
using Backend_AuraNeuro.API.Neurologist.Domain.Services.Queries;
using NeurologistProfile = Backend_AuraNeuro.API.Neurologist.Domain.Model.Aggregates.Neurologist;

namespace Backend_AuraNeuro.API.Neurologist.Application.Internal.QueryServices;

/// <summary>
/// Implementation of neurologist query handlers.
/// </summary>
public class NeurologistQueryService : INeurologistQueryService
{
    private readonly INeurologistRepository neurologistRepository;

    public NeurologistQueryService(INeurologistRepository neurologistRepository)
    {
        this.neurologistRepository = neurologistRepository;
    }

    public async Task<IEnumerable<NeurologistProfile>> Handle(GetAllNeurologistsQuery query)
    {
        return await neurologistRepository.ListAsync();
    }

    public async Task<NeurologistProfile?> Handle(GetNeurologistByIdQuery query)
    {
        return await neurologistRepository.FindByIdAsync(query.NeurologistId);
    }
}