using Auction.Contracts.Mapping.Profiles;
using Auction.Contracts.Validation;
using Auction.Core.Interfaces.Data;
using Auction.Infrastructure;
using Auction.Infrastructure.Database;
using Auction.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Auction.Api.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAuctionsRepository, AuctionsRepository>();
        services.AddScoped<IBidsRepository, BidsRepository>();

        services.AddValidation();
        services.AddAutoMapper(typeof(UserProfile).Assembly);
        services.AddAutoMapper(typeof(AuctionProfile).Assembly);
        return services;
    }
}