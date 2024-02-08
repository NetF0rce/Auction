using Auction.Domain.Enums;

namespace Auction.Contracts.DTO;

public class AuctionResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> ImageUrls { get; set; }
    public double Score { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public long AuctionistUserId { get; set; }
    public string AuctionistUsername { get; set; }
    public AuctionStatus Status { get; set; }
    public bool IsPaied { get; set; }
}
