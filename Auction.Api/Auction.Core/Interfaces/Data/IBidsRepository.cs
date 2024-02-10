using Auction.Domain.Entities;

namespace Auction.Core.Interfaces.Data;

public interface IBidsRepository : IBaseRepository<Bid>
{
    Task<IEnumerable<Bid>> GetBidsByAuctionIdAsync(long id);
}
