using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Repositories;
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC;
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC.Configuration;
namespace Backend_AuraNeuro.API.NeurologicalHealth.Infrastructure.Persistence.EFC.Repositories;

public class NeurologicalHealthRepository(AppDbContext appDbContext): BaseRepository<NeuroAssessment>(appDbContext), INeurologicalHealthRepository
{
    public async Task<NeuroAssessment?> FindByNumberAsync(string number)
    {
        return await appDbContext.Set<NeuroAssessment>().FindAsync(number);
    }
}