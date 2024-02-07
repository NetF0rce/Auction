using Auction.Contracts.DTO.Authorization;
using Auction.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using IAuthorizationService = Auction.Core.Interfaces.Authorization.IAuthorizationService;

namespace Auction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthorizationService authorizationService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthorizationResponse>> Register(RegisterDto registerDto)
    {
        return Ok(await authorizationService.RegisterUserAsync(registerDto, UserRole.Customer));
    }
        
    [HttpPost("register-admin")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<AuthorizationResponse>> RegisterAdmin(RegisterDto registerDto)
    {
        return Ok(await authorizationService.RegisterUserAsync(registerDto, UserRole.Admin));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthorizationResponse>> Login(LoginDto loginDto)
    {
        return Ok(await authorizationService.LoginAsync(loginDto));
    }
        
    [HttpPost("external-login")]
    public async Task<IActionResult> ExternalLogin([FromBody]ExternalAuthDto externalAuth)
    {
        return Ok(await authorizationService.ExternalLogin(externalAuth));
    }
}