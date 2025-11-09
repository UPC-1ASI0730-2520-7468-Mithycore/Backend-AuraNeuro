using Backend_AuraNeuro.API.Shared.Domain.Repositories;
using Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Backend_AuraNeuro.API.Shared.Infrastructure.Persistence.EFC;

public class BaseRepository<TEntity>(AppDbContext appDbContext): IBaseRepository<TEntity> where TEntity : class
{
    public async Task AddAsync(TEntity entity)
    {
        await appDbContext.Set<TEntity>().AddAsync(entity);
    }

    public async Task<IEnumerable<TEntity>> ListAsync()
    {
        return await appDbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> FindByIdAsync(int id)
    {
        return await appDbContext.Set<TEntity>().FindAsync(id);
    }

    public void Update(TEntity entity)
    {
        appDbContext.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        appDbContext.Set<TEntity>().Remove(entity);
    }
}