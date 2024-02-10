using Auction.Contracts.DTO.Profile;
using Auction.Core.Interfaces.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController(IProfileService profileService) : Controller
{
   [HttpGet]
   [Authorize]
   public async Task<ActionResult<ProfileResponse>> GetProfile()
   {
      return Ok(await profileService.GetProfile());
   }
   
   [HttpPut]
   [Authorize]
   public async Task<ActionResult<ProfileResponse>> UpdateProfile([FromForm] ProfileUpdateRequest profileUpdateRequest)
   {
      return Ok(await profileService.UpdateProfile(profileUpdateRequest));
   }
}