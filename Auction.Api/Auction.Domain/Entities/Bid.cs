using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auction.Domain.Entities;

public class Bid : EntityBase
{
    [Required]
    public long AuctionId { get; set; }

    [Required]
    public long BidderId { get; set; }

    [Required]
    public DateTime DateAndTime { get; set; }

    [Required]
    public decimal Amout { get; set; }

    [Required]
    public bool IsWinning { get; set; }

    [ForeignKey(nameof(AuctionId))]
    public Auction Auction { get; set; }

    [ForeignKey(nameof(BidderId))]
    public User Bidder { get; set; }
}
