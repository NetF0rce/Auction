using Auction.Core.Interfaces.Data;
using Auction.Domain.Entities;
using Auction.Infrastructure.Database;

namespace Auction.Infrastructure.Repositories;

public class AuctionImagesRepository : BaseRepository<AuctionImage>, IAuctionImagesRepository
{
    public AuctionImagesRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
