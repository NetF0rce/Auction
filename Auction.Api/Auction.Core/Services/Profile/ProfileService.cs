using Auction.Contracts.DTO.Profile;
using Auction.Core.Interfaces.Images;
using Auction.Core.Interfaces.Profile;
using Auction.Core.Interfaces.UserAccessor;
using Auction.Core.Interfaces.Users;
using AutoMapper;

namespace Auction.Core.Services.Profile;

public class ProfileService(IUserRepository userRepository, IMapper mapper, IImagesService imagesService, IUserAccessor userAccessor): IProfileService
{
    public async Task<ProfileResponse> GetProfile()
    {
        var user = await userRepository.GetByIdAsync(userAccessor.GetCurrentUserId());

        if (user is null)
            throw new KeyNotFoundException("Cannot find user with such id");

        return mapper.Map<ProfileResponse>(user);
    }

    public async Task<ProfileResponse> UpdateProfile(ProfileUpdateRequest profileUpdateRequest)
    {
        var user = await userRepository.GetByIdAsync(userAccessor.GetCurrentUserId());
        
        if (user is null)
            throw new KeyNotFoundException("Cannot find user with such id");
        
        var userWithTheSameNick = await userRepository.GetByUserNameAsync(profileUpdateRequest.Username);
            
        if (userWithTheSameNick is not null && userWithTheSameNick.Id != user.Id)
            throw new ArgumentException("This nick has alrady been taken");

        user.Username = profileUpdateRequest.Username;
        
        if (profileUpdateRequest.Image is not null)
        {
            if (user.PublicId is not null)
                await imagesService.DeleteImageAsync(user.PublicId);

            var imageResult = await imagesService.AddImageAsync(profileUpdateRequest.Image);
            user.PublicId = imageResult.PublicId;
            user.ImageUrl = imageResult.SecureUrl.AbsoluteUri;
        }

        var response = await userRepository.UpdateAsync(user);

        return mapper.Map<ProfileResponse>(response);
    }
}