using Auction.Contracts.DTO.User;

namespace Auction.Contracts.DTO.Authorization;

public class AuthorizationResponse
{
    public UserDto UserDto { get; set; }
    public string Token { get; set; }
}