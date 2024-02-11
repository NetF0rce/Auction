namespace Auction.Contracts.DTO.Bid;

public class BidResponse
{
    public long Id { get; set; }
    public long AuctionId { get; set; }
    public long BidderId { get; set; } 
    public DateTime DateAndTime { get; set; }
    public decimal Amount { get; set; }
    public bool IsWinning { get; set; } 
    public string BidderName { get; set; }
}