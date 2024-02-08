namespace Auction.Contracts.DTO.User;

public class UserDto
{
    public string Username { get; set; }

    public string FullName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string Email { get; set; }
    
    public string Role { get; set; }

    public string ImageUrl { get; set; }
}