using Auction.Core.Interfaces;
using Auction.Core.Interfaces.Auctions;
using Auction.Core.Interfaces.UserAccessor;
using Auction.Core.Services.Auctions;
using Auction.Core.Interfaces.Comments;
using Auction.Core.Services.CommentService;
using Auction.Core.Services.Score;
using Auction.Core.Services.UserAccessor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Core.Extensions.DependencyInjection;

public static class DependencyInjectionCore
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUserAccessor, UserAccessor>();
      
        services.AddScoped<IAuctionsService, AuctionsService>();
        services.AddScoped<IAuctionsVerificationService, AuctionsVerificationService>();
        services.AddScoped<IScoreService, ScoreService>();
        services.AddScoped<ICommentsService, CommentsService>();
      
        return services;
    }
}