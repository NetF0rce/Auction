using Auction.Core.Specifications;
using Auction.Domain.Entities;

namespace Auction.Core.Interfaces.Data;

public interface IBaseRepository<TEntity> where TEntity : EntityBase
{
    Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification);
    Task<TEntity?> GetByIdAsync(long id);
    Task<bool> IsExistAsync(long id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<bool> DeleteByIdAsync(long id);
}