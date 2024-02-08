using System.ComponentModel.DataAnnotations;

namespace Auction.Contracts.DTO.Authorization;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}