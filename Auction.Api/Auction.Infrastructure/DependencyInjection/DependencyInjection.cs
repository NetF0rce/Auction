using Auction.Core.Interfaces.Comments;
using Auction.Core.Interfaces.Data;
using Auction.Domain.Entities;
using Auction.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICommentsRepository, CommentsRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}