using System.Text;
using Auction.Core.Interfaces.Comments;
using Auction.Core.Interfaces.Data;
using Auction.Core.Interfaces.Users;
using Auction.Domain.Entities;
using Auction.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Auction.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ICommentRepository, CommentsRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuctionsRepository, AuctionsRepository>();
        services.AddScoped<IBidsRepository, BidsRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuctionImagesRepository,AuctionImagesRepository>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        return services;
    }
}