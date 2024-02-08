﻿using Auction.Contracts.DTO;

namespace Auction.Core.Interfaces.Auctions;

public interface IAuctionsService
{
    Task<ListModel<AuctionResponse>> GetAuctionsListAsync(AuctionFiltersDTO filters);
    Task<AuctionResponse> GetAuctionByIdAsync(long id);
    Task PublishAuctionAsync(PublishAuctionRequest auction);
    Task CancelAuctionAsync(long id);
    Task RecoverAuctionAsync(long id);
    Task FinishAuctionAsync(long id);
    Task ConfirmPaymentForAuctionAsync(long id);
}