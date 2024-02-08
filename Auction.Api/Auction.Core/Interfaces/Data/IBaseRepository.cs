using Auction.Core.Specifications;
using Auction.Domain.Entities;
using System.Linq.Expressions;

namespace Auction.Core.Interfaces.Data;

public interface IBaseRepository<TEntity> where TEntity : EntityBase
{
    Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification);
    Task<TEntity?> GetByIdAsync(long id);
    Task<bool> IsExistAsync(long id);
    Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<bool> DeleteByIdAsync(long id);
    Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities);
}