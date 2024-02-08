using Auction.Contracts.DTO.Authorization;
using Auction.Domain.Enums;

namespace Auction.Core.Interfaces.Authorization;

public interface IAuthorizationService
{
    Task<AuthorizationResponse> RegisterUserAsync(RegisterDto registerDto, UserRole role);
    Task<AuthorizationResponse> LoginAsync(LoginDto loginDto);
    Task<AuthorizationResponse> ExternalLogin(ExternalAuthDto externalAuth);
}