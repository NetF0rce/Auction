using Microsoft.AspNetCore.Authorization;

namespace Auction.Contracts.DTO;

public record GetAuctionScoreDto
{
    public long Id { get; set; }
    public required long AuctionId { get; set; }
    public required long UserId { get; set; }
    public required int Score { get; set; }
};