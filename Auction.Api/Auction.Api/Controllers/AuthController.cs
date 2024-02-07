using Auction.Contracts.DTO.Authorization;
using Auction.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IAuthorizationService = Auction.Core.Interfaces.Authorization.IAuthorizationService;

namespace Auction.Api.Controllers;

public class AuthController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;

    public AuthController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<AuthorizationResponse>> Register(RegisterDto registerDto)
    {
        return Ok(await _authorizationService.RegisterUserAsync(registerDto, UserRole.Customer));
    }
        
    [HttpPost("register-admin")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<AuthorizationResponse>> RegisterAdmin(RegisterDto registerDto)
    {
        return Ok(await _authorizationService.RegisterUserAsync(registerDto, UserRole.Admin));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthorizationResponse>> Login(LoginDto loginDto)
    {
        return Ok(await _authorizationService.LoginAsync(loginDto));
    }
        
    [HttpPost("external-login")]
    public async Task<IActionResult> ExternalLogin([FromBody]ExternalAuthDto externalAuth)
    {
        return Ok(await _authorizationService.ExternalLogin(externalAuth));
    }
}