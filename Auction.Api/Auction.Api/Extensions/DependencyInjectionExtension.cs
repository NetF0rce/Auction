using Auction.Contracts.Mapping.Profiles;
using Auction.Contracts.Validation;
using Auction.Core.Extensions.DependencyInjection;
using Auction.Infrastructure.Database;
using Auction.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Auction.Api.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddCore(configuration);
        services.AddInfrastructure();
        services.AddValidation();
        services.AddAutoMapper(typeof(UserProfile).Assembly);
        return services;
    }
}