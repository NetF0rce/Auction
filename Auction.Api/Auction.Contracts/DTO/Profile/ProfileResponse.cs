namespace Auction.Contracts.DTO.Profile;

public class ProfileResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string FullName { get; set; }
    public bool IsDeleted { get; set; }
    public string? ImageUrl { get; set; }
}