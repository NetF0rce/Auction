using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Auction.Contracts.DTO.Profile;

public class ProfileUpdateRequest
{
    [Required]
    public Guid Id { get; set; }
    [MinLength(2)]
    [MaxLength(25)]
    [Required]
    public string? FullName { get; set; }
    private DateOnly? DateOfBirth { get; set; }
    public IFormFile? Image { get; set; }
}