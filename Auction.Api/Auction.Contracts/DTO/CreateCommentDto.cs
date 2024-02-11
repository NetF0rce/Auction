namespace Auction.Contracts.Dto;

public record CreateCommentDto
{
    public required long AuctionId { get; set; }
    public required string MainText { get; set; }
};