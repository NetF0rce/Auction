using Auction.Core.Interfaces.Data;
using Auction.Domain.Entities;
using Auction.Infrastructure.Database;
using AutoMapper;

namespace Auction.Infrastructure.Repositories;

public class BidsRepository : BaseRepository<Bid>, IBidsRepository
{
    public BidsRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
