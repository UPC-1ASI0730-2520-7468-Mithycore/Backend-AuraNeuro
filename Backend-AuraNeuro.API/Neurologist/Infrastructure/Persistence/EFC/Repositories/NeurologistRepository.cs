using Backend_AuraNeuro.API.Neurologist.Domain.Repositories;
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;
using NeurologistProfile = Backend_AuraNeuro.API.Neurologist.Domain.Model.Aggregates.Neurologist;

namespace Backend_AuraNeuro.API.Neurologist.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Neurologist repository implementation.
/// </summary>
public class NeurologistRepository : INeurologistRepository
{
    private readonly AppDbContext context;
    private readonly DbSet<NeurologistProfile> neurologists;

    public NeurologistRepository(AppDbContext context)
    {
        this.context = context;
        neurologists = context.Set<NeurologistProfile>();
    }

    // ===== IBaseRepository<NeurologistProfile> implementation =====

    public async Task<IEnumerable<NeurologistProfile>> ListAsync()
    {
        return await neurologists.ToListAsync();
    }

    public async Task<NeurologistProfile?> FindByIdAsync(long id)
    {
        return await neurologists.FindAsync(id);
    }

    public async Task AddAsync(NeurologistProfile entity)
    {
        await neurologists.AddAsync(entity);
    }

    public void Update(NeurologistProfile entity)
    {
        neurologists.Update(entity);
    }

    public void Remove(NeurologistProfile entity)
    {
        neurologists.Remove(entity);
    }

    // ===== Custom methods from INeurologistRepository =====

    public async Task<NeurologistProfile?> FindByUserIdAsync(long userId)
    {
        return await neurologists
            .FirstOrDefaultAsync(n => n.UserId == userId);
    }

    public async Task<NeurologistProfile?> FindByLicenseNumberAsync(string licenseNumber)
    {
        return await neurologists
            .FirstOrDefaultAsync(n => n.LicenseNumber.Value == licenseNumber);
    }

    public async Task<IEnumerable<NeurologistProfile>> FindBySpecialtyAsync(string specialty)
    {
        return await neurologists
            .Where(n => n.Specialties.Contains(specialty))
            .ToListAsync();
    }
}
