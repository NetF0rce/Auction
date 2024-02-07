using Auction.Core.Interfaces.Data;
using Auction.Core.Specifications;
using Auction.Domain.Entities;
using Auction.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Auction.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntityBase
{
    protected readonly ApplicationDbContext context;

    protected BaseRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification)
    {
        return await context.Set<TEntity>()
            .ApplySpecification(specification)
            .ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(long id)
    {
        var entity = await context.Set<TEntity>()
            .FirstOrDefaultAsync(e => e.Id == id);

        return entity;
    }

    public async Task<bool> IsExistAsync(long id)
    {
        var entity = await context.Set<TEntity>()
            .FirstOrDefaultAsync(e => e.Id == id);

        return entity is not null;
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        var entityEntry = (await context.Set<TEntity>().AddAsync(entity)).Entity;
        await context.SaveChangesAsync();
        return entityEntry;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var updatedModel = context.Set<TEntity>().Update(entity).Entity;
        await context.SaveChangesAsync();
        return updatedModel;
    }

    public virtual async Task<bool> DeleteByIdAsync(long id)
    {
        var entity = await GetByIdAsync(id);

        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}