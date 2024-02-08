using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using Auction.Contracts.DTO.Authorization;
using Auction.Contracts.DTO.User;
using Auction.Core.Helpers.Jwt;
using Auction.Core.Interfaces.Authorization;
using Auction.Core.Interfaces.Data;
using Auction.Core.Services.Abstract;
using Auction.Domain.Entities;
using Auction.Domain.Enums;
using AutoMapper;

namespace Auction.Core.Services.Authorization;

public class AuthorizationService : BaseService, IAuthorizationService
{
    private readonly ITokenService _tokenService;
    private readonly JwtHelper _jwtHelper;

    public AuthorizationService(
        ITokenService tokenService,
        IMapper mapper,
        JwtHelper jwtHelper,
        IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
    {
        _tokenService = tokenService;
        _jwtHelper = jwtHelper;
    }

    public async Task<AuthorizationResponse> LoginAsync(LoginDto loginDto)
    {
        var user = await UnitOfWork.UserRepository.GetByEmailAsync(loginDto.Email);

        if (user is null) 
            throw new AuthenticationException("Invalid Username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        if (computedHash.Where((t, i) => t != user.PasswordHash[i]).Any())
        {
            throw new AuthenticationException("Invalid Password");
        }

        return new AuthorizationResponse
        {
            UserDto = Mapper.Map<UserDto>(user),
            Token = _tokenService.CreateToken(user)
        };
    }

    public async Task<AuthorizationResponse> RegisterUserAsync(RegisterDto registerDto, UserRole role)
    {
        var userWithSameNickname = await UnitOfWork.UserRepository.GetByUserNameAsync(registerDto.Username);

        if (userWithSameNickname != null)
        {
            throw new AuthenticationException("Here is already user with the same nickname");
        }

        var userWithSameEmail = await UnitOfWork.UserRepository.GetByEmailAsync(registerDto.Email);

        if (userWithSameEmail != null)
        {
            throw new AuthenticationException("Here is already user with the same email");
        }

        var user = Mapper.Map<User>(registerDto);

        using var hmac = new HMACSHA512();

        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
        user.PasswordSalt = hmac.Key;
        user.Role = role;
        user.IsDeleted = false;
        
        await UnitOfWork.UserRepository.AddAsync(user);

        return new AuthorizationResponse
        {
            UserDto = Mapper.Map<UserDto>(user),
            Token = _tokenService.CreateToken(user)
        };
    }

    public async Task<AuthorizationResponse> ExternalLogin(ExternalAuthDto externalAuth)
    {
        var payload = await _jwtHelper.VerifyGoogleToken(externalAuth);

        if (payload == null)
            throw new ArgumentException("Invalid External Authentication.");

        var user = await UnitOfWork.UserRepository.GetByEmailAsync(payload.Email);

        if (user is null)
        {
            user = new User { Email = payload.Email, Username = payload.Email, ImageUrl = payload.Picture };
            await UnitOfWork.UserRepository.AddAsync(user);
        }

        if (user is null)
            throw new ArgumentException("Invalid External Authentication.");

        var token = _tokenService.CreateToken(user);
        return new AuthorizationResponse()
        {
            UserDto = Mapper.Map<UserDto>(user),
            Token = token
        };
    }
}