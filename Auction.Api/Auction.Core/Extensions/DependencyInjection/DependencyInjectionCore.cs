using Auction.Core.Helpers.Jwt;
using Auction.Core.Interfaces.Auctions;
using Auction.Core.Interfaces.Authorization;
using Auction.Core.Interfaces.UserAccessor;
using Auction.Core.Services.Auctions;
using Auction.Core.Interfaces.Comments;
using Auction.Core.Services;
using Auction.Core.Services.Authorization;
using Auction.Core.Services.UserAccessor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Relaxinema.Core.Services;

namespace Auction.Core.Extensions.DependencyInjection;

public static class DependencyInjectionCore
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUserAccessor, UserAccessor>();
      
        services.AddScoped<IAuctionsService, AuctionsService>();
        services.AddScoped<IAuctionsVerificationService, AuctionsVerificationService>();
      
        services.AddScoped<ICommentsService, CommentsService>();
        
        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddTransient<JwtHelper>();
      
        return services;
    }
}