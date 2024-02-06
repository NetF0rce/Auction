using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auction.Domain.Entities;

public class AuctionComment : EntityBase
{
    [Required]
    public long AuctionId { get; set; }

    [Required]
    public long UserId { get; set; }

    [Required]
    public DateTime DateTime { get; set; }

    [Required]
    [MaxLength(300)]
    public string MainText { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    [ForeignKey(nameof(AuctionId))]
    public Auction Auction { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}
