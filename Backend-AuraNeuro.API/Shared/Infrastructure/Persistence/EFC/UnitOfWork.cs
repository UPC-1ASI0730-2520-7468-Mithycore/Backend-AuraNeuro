using Backend_AuraNeuro.API.Shared.Domain.Repositories;
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC;

public class UnitOfWork(AppDbContext appDbContext): IUnitOfWork
{
    public async Task CompleteAsync()
    {
        await appDbContext.SaveChangesAsync();
    }
}