using Auction.Core.Interfaces.Data;
using Auction.Domain.Entities;
using Auction.Infrastructure.Database;

namespace Auction.Infrastructure.Repositories;

public class ScoreRepository : BaseRepository<AuctionScore>, IScoreRepository
{
    public ScoreRepository(ApplicationDbContext context) : base(context)
    {
    }
}