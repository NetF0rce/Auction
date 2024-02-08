using Auction.Contracts.Mapping.Profiles;
using Auction.Contracts.Validation;
using Auction.Core.Helpers.Jwt;
using Auction.Core.Interfaces.Authorization;
using Auction.Core.Interfaces.Data;
using Auction.Core.Interfaces.UserAccessor;
using Auction.Core.Interfaces.Users;
using Auction.Core.Services.Authorization;
using Auction.Core.Services.UserAccessor;
using Auction.Core.Extensions.DependencyInjection;
using Auction.Infrastructure.DependencyInjection;
using Auction.Infrastructure;
using Auction.Infrastructure.Database;
using Auction.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Relaxinema.Core.Services;

namespace Auction.Api.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddTransient<JwtHelper>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IAuctionsRepository, AuctionsRepository>();
        services.AddScoped<IBidsRepository, BidsRepository>();
        services.AddScoped<ICommentsRepository, CommentsRepository>();
        services.AddCore(configuration);
        services.AddInfrastructure();

        services.AddValidation();
        services.AddAutoMapper(typeof(UserProfile).Assembly);

        return services;
    }
}