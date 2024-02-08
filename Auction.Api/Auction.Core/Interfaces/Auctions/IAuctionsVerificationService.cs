using Auction.Contracts.DTO;

namespace Auction.Core.Interfaces.Auctions;

public interface IAuctionsVerificationService
{
    Task<IEnumerable<AuctionResponse>> GetNotApprovedAuctionsAsync();
    Task<AuctionResponse> GetNotApprovedAuctionByIdAsync(long id);
    Task ApproveAuctionAsync(long id);
    Task RejectAuctionAsync(RejectAuctionRequest request);
}
