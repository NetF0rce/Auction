using System.ComponentModel.DataAnnotations;
using Auction.Domain.Enums;

namespace Auction.Contracts.DTO.Authorization;

public class RegisterDto
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
    public string Password { get; set; }
}