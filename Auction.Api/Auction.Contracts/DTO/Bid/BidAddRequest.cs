namespace Auction.Contracts.DTO.Bid;

public class BidAddRequest
{
    public long AuctionId { get; set; }
    public long BidderId { get; set; }
    public DateTime DateAndTime { get; set; }
    public decimal Amount { get; set; }
}