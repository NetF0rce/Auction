using Auction.Contracts.Enums;
using Auction.Domain.Entities;
using System.Linq.Expressions;

namespace Auction.Core.Specifications;

public class SpecificationBase<T> : ISpecification<T> where T : EntityBase
{
    public virtual Expression<Func<T, bool>>? Predicate { get; set; }
    public virtual Expression<Func<T, object>>? SortBy { get; set; }
    public virtual Func<IQueryable<T>, IQueryable<T>>? Include { get; set; }
    public virtual SortDirection SortOrder { get; set; }
    public virtual bool IsPaginationEnabled { get; set; }
    public virtual int Skip { get; set; }
    public virtual int Take { get; set; }

    public SpecificationBase(Expression<Func<T, bool>>? predicate = null,
                             Expression<Func<T, object>>? sortBy = null,
                             SortDirection orderDirection = SortDirection.ASC,
                             Func<IQueryable<T>, IQueryable<T>>? include = null)
    {
        this.Predicate = predicate;
        this.SortBy = sortBy;
        this.SortOrder = orderDirection;
        this.Include = include;
    }

    public SpecificationBase(int take, int skip,
                         Expression<Func<T, bool>>? predicate = null,
                         Expression<Func<T, object>>? sortBy = null,
                         SortDirection orderDirection = SortDirection.ASC,
                         Func<IQueryable<T>, IQueryable<T>>? include = null)
    {
        this.Predicate = predicate;
        this.SortBy = sortBy;
        this.SortOrder = orderDirection;
        this.Include = include;

        this.IsPaginationEnabled = true;

        this.Take = take;
        this.Skip = skip;
    }
}
