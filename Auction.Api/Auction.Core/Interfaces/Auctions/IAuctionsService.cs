using Auction.Contracts.DTO;

namespace Auction.Core.Interfaces.Auctions;

public interface IAuctionsService
{
    Task<ListModel<AuctionResponse>> GetAuctionsListAsync();
    Task<AuctionResponse> GetAuctionByIdAsync(long id);
    Task<AuctionResponse> PublishAuctionAsync(PublishAuctionRequest auction);
    Task EditAuctionAsync(long id, EditAuctionRequest auction);
    Task CancelAuctionAsync(long id);
    Task RecoverAuctionAsync(long id);
    Task FinishAuctionAsync(long id);
    Task ConfirmPaymentForAuctionAsync(long id);
}
