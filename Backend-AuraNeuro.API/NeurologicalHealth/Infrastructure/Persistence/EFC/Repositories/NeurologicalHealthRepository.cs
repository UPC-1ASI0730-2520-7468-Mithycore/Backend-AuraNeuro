using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Model.Aggregates;
using Backend_AuraNeuro.API.NeurologicalHealth.Domain.Repositories;
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC;
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Backend_AuraNeuro.API.NeurologicalHealth.Infrastructure.Persistence.EFC.Repositories;

public class NeurologicalHealthRepository(AppDbContext appDbContext): BaseRepository<NeuroAssessment>(appDbContext), INeurologicalHealthRepository
{
    public async Task<IEnumerable<NeuroAssessment>> FindByPatientIdAsync(long patientId)
    {
        return await appDbContext.Set<NeuroAssessment>().Where(e => e.PatientId == patientId).ToListAsync();
    }

    public async Task<IEnumerable<NeuroAssessment>> FindByNeurologistIdAsync(long neurologistId)
    {
        return await appDbContext.Set<NeuroAssessment>().Where(e => e.NeurologistId == neurologistId).ToListAsync();
    }
}