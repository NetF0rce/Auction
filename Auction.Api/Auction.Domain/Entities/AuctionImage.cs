using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auction.Domain.Entities;

public class AuctionImage : EntityBase
{
    [Required]
    public long AuctionId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Url { get; set; }

    [Required]
    [MaxLength(500)]
    public string PublicId { get; set; }

    [ForeignKey(nameof(AuctionId))]
    public Auction Auction { get; set; }
}
