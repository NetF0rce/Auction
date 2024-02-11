using System.Security.Authentication;
using System.Security.Claims;
using Auction.Core.Interfaces.UserAccessor;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Auction.Core.Services.UserAccessor;

public class UserAccessor(IHttpContextAccessor httpContextAccessor, ILogger<UserAccessor> logger)
    : IUserAccessor
{
    public long GetCurrentUserId()
    {
        var id = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if(id is null)
            throw new AuthenticationException("User is not authorized");

        long userId;
        try
        {
            userId = long.Parse(id);
        }
        catch (Exception e)
        {
            logger.LogError("Cannot parse user id");
            throw new ApplicationException("User id is messed up");
        }
        
        return userId;
    }
}