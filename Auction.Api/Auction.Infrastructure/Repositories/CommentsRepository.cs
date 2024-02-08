using Auction.Core.Interfaces.Data;
using Auction.Domain.Entities;
using Auction.Infrastructure.Database;

namespace Auction.Infrastructure.Repositories;

public class CommentsRepository : BaseRepository<AuctionComment>, ICommentRepository
{
    public CommentsRepository(ApplicationDbContext context) : base(context)
    {
    }
}