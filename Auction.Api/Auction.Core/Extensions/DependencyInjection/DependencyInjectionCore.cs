using Auction.Core.Interfaces;
using Auction.Core.Helpers.Jwt;
using Auction.Core.Interfaces.Auctions;
using Auction.Core.Interfaces.Authorization;
using Auction.Core.Interfaces.Bids;
using Auction.Core.Interfaces.UserAccessor;
using Auction.Core.Services.Auctions;
using Auction.Core.Interfaces.Comments;
using Auction.Core.Services.Authorization;
using Auction.Core.Services.Bids;
using Auction.Core.Services.CommentService;
using Auction.Core.Services.Score;
using Auction.Core.Services.UserAccessor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Relaxinema.Core.Services;
using Auction.Core.Interfaces.Images;
using Auction.Core.Interfaces.Profile;
using Auction.Core.Services.Images;
using Auction.Core.Services.Profile;

namespace Auction.Core.Extensions.DependencyInjection;

public static class DependencyInjectionCore
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUserAccessor, UserAccessor>();
      
        services.AddScoped<IAuctionsService, AuctionsService>();
        services.AddScoped<IAuctionsVerificationService, AuctionsVerificationService>();
        services.AddScoped<IImagesService, ImagesService>();
        services.AddScoped<IScoreService, ScoreService>();
        services.AddScoped<ICommentsService, CommentsService>();
        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IBidService, BidService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddTransient<JwtHelper>();
      
        return services;
    }
}