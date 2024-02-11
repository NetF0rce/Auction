using Auction.Contracts.DTO.Profile;

namespace Auction.Core.Interfaces.Profile;

public interface IProfileService
{
    Task<ProfileResponse> GetProfile();
    Task<ProfileResponse> UpdateProfile(ProfileUpdateRequest profileUpdateRequest);
}