using System.ComponentModel.DataAnnotations;

namespace Auction.Contracts.DTO;

public class RejectAuctionRequest
{
    [Required]
    public long AuctionId { get; set; }

    [Required]
    public string RejectionReason { get; set; }
}
