using Auction.Contracts.DTO.Bid;

namespace Auction.Core.Interfaces.Bids;

public interface IBidService
{
    Task<BidResponse> SetNewBid(BidAddRequest bidAddRequest);
    Task<IEnumerable<BidResponse>> GetBidsByAuctionIdAsync(long id);
}