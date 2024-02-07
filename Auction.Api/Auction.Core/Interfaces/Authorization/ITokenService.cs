using Auction.Domain.Entities;

namespace Auction.Core.Interfaces.Authorization;

public interface ITokenService
{
    string CreateToken(User user);
}
