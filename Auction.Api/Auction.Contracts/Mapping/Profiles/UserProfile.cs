using Auction.Contracts.DTO.Authorization;
using Auction.Contracts.DTO.Profile;
using Auction.Contracts.DTO.User;
using Auction.Domain.Entities;
using AutoMapper;

namespace Auction.Contracts.Mapping.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(ud => ud.Role, u => u.MapFrom(us => us.Role.ToString()));
        CreateMap<RegisterDto, User>()
            .ForMember(u => u.DateOfBirth, r => r.MapFrom(pd => DateOnly.FromDateTime(pd.DateOfBirth)));
        CreateMap<User, ProfileResponse>();
        CreateMap<ProfileUpdateRequest, User>();
    }
}