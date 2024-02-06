using Auction.Core.Validation;
using Auction.Infrastructure.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Auction.Api.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddValidation();
        return services;
    }
}