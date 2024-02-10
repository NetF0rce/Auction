namespace Auction.Contracts.DTO;

public record CreateOrUpdateScoreDto
{
    public required long AuctionId { get; set; }
    public required int Score { get; set; }
};