using Auction.Domain.Entities;

namespace Auction.Core.Specifications;

public static class SpecificationHandlerExt
{
    public static IQueryable<T> ApplySpecification<T>(this IQueryable<T> query, ISpecification<T> specification)
        where T : EntityBase
    {
        if (specification.Predicate is not null)
        {
            query = query.Where(specification.Predicate);
        }

        if (specification.SortBy is not null)
        {
            switch (specification.SortOrder)
            {
                case Enums.SortDirection.ASC:
                    query = query.OrderBy(specification.SortBy);
                    break;
                case Enums.SortDirection.DESC:
                    query = query.OrderByDescending(specification.SortBy);
                    break;
            }
        }

        if (specification.Include is not null)
        {
            query = specification.Include(query);
        }

        if (specification.IsPaginationEnabled)
        {
            query = query.Skip(specification.Skip)
                     .Take(specification.Take);
        }

        return query;
    }
}
