using Auction.Core.Interfaces.Data;
using Auction.Domain.Entities;

namespace Auction.Core.Interfaces.Users;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUserNameAsync(string userName);
}