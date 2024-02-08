using Auction.Contracts.Enums;
using Auction.Domain.Entities;
using System.Linq.Expressions;

namespace Auction.Core.Specifications;

/// <summary>
/// Specification for filtering, sorting and pagination the set of entities.
/// </summary>
/// <typeparam name="T">Type of entity to apply specifications.</typeparam>
public interface ISpecification<T> where T : EntityBase
{
    /// <summary>
    /// Expression for filtering.
    /// </summary>
    Expression<Func<T, bool>>? Predicate { get; set; }

    /// <summary>
    /// Expression for entity field to sort by.
    /// </summary>
    Expression<Func<T, object>>? SortBy { get; set; }

    /// <summary>
    /// Expression for include entities by foreign key.
    /// </summary>
    Func<IQueryable<T>, IQueryable<T>>? Include { get; set; }

    /// <summary>
    /// Direction to sort in.
    /// </summary>
    SortDirection SortOrder { get; set; }

    /// <summary>
    /// Flag that identifies whether the pagination is enabled.
    /// </summary>
    bool IsPaginationEnabled { get; set; }

    /// <summary>
    /// Amount to skip elements.
    /// </summary>
    int Skip { get; set; }

    /// <summary>
    /// Amount to take elements.
    /// </summary>
    int Take { get; set; }
}
