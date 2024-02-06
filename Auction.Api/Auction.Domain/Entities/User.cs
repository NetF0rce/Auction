using Auction.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Auction.Domain.Entities;

public class User : EntityBase
{
    [Required]
    [MaxLength(30)]
    public string Username { get; set; }

    [Required]
    [MaxLength(30)]
    public string FullName { get; set; }

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    [MaxLength(30)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(300)]
    public string PasswordHash { get; set; }

    [Required]
    [MaxLength(300)]
    public string PasswordSalt { get; set; }

    [Required]
    public UserRole Role { get; set; }

    [Required]
    [MaxLength(500)]
    public string ImageUrl { get; set; }

    [Required]
    [MaxLength(500)]
    public string PublicId { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    public ICollection<Auction> Auctions { get; set; }

    public ICollection<Bid> Bids { get; set; }
}
