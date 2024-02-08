namespace Auction.Contracts.Dto;

public record GetCommentDto
{
    public required long Id { get; set; }
    public required long AuctionId { get; set; }
    public required long UserId { get; set; }
    public required string UserName { get; set; }
    public required DateTime DateTime { get; set; }
    public required string MainText { get; set; }
};