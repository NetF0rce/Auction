using Auction.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Auction.Domain.Entities;

public class Auction : EntityBase
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    public long AuctionistId { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; }

    [AllowNull]
    public DateTime StartDateTime { get; set; }

    [AllowNull]
    public DateTime FinishDateTime { get; set; }

    [Required]
    public TimeSpan FinishInterval { get; set; }

    [Required]
    [Range(0, 10e10)]
    public decimal StartPrice { get; set; }

    [Required]
    public AuctionStatus Status { get; set; }

    [Required]
    public bool IsPaid { get; set; }

    [ForeignKey(nameof(AuctionistId))]
    public User Auctionist { get; set; }

    public ICollection<AuctionImage> Images { get; set; }

    public ICollection<AuctionComment> Comments { get; set; }

    public ICollection<AuctionScore> Scores { get; set; }

    public ICollection<Bid> Bids { get; set; }
}
