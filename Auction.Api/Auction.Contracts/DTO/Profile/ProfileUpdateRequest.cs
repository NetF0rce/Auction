using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Auction.Contracts.DTO.Profile;

public class ProfileUpdateRequest
{
    [MaxLength(30)]
    public string? FullName { get; set; }

    [MaxLength(30)]
    public string Username { get; set; }
    
    private DateOnly? DateOfBirth { get; set; }
    
    public IFormFile? Image { get; set; }
}