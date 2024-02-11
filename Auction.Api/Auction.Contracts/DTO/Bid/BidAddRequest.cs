namespace Auction.Contracts.DTO.Bid;

public class BidAddRequest
{
    public long AuctionId { get; set; }
    public long? BidderId { get; set; }
    public decimal Amount { get; set; }
}