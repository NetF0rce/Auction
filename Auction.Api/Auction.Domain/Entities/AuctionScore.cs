using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auction.Domain.Entities;

public class AuctionScore : EntityBase
{
    [Required]
    public long AuctionId { get; set; }

    [Required]
    public long UserId { get; set; }

    [Required]
    [Range(1, 10)]
    public int Score { get; set; }

    [ForeignKey(nameof(AuctionId))]
    public Auction Auction { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}