using Auction.Core.Interfaces.Auctions;
using Auction.Core.Interfaces.UserAccessor;
using Auction.Core.Services.Auctions;
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

        return services;
    }
}