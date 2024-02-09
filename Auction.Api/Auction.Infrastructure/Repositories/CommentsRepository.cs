using Auction.Core.Interfaces.Data;
using Auction.Domain.Entities;
using Auction.Infrastructure.Database;

namespace Auction.Infrastructure.Repositories;

public class CommentsRepository : BaseRepository<AuctionComment>, ICommentsRepository
{
    public CommentsRepository(ApplicationDbContext context) : base(context)
    {
    }
}