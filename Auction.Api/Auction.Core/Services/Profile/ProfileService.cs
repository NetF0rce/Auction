using Auction.Contracts.DTO.Profile;
using Auction.Core.Interfaces.Profile;
using Auction.Core.Interfaces.Users;
using AutoMapper;

namespace Auction.Core.Services.Profile;

public class ProfileService(IUserRepository userRepository, IMapper mapper): IProfileService
{
    
    public async Task<ProfileResponse> GetProfile(long userId)
    {
        throw new NotImplementedException();
    }

    public async Task<ProfileResponse> UpdateProfile(ProfileUpdateRequest profileUpdateRequest)
    {
        throw new NotImplementedException();
    }
}