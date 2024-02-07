﻿using Auction.Core.Interfaces.Comments;
using Auction.Core.Interfaces.UserAccessor;
using Auction.Core.Services;
using Auction.Core.Services.UserAccessor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Core.Extensions.DependencyInjection;

public static class DependencyInjectionCore
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddScoped<ICommentsService, CommentsService>();
        return services;
    }
}