using BestPracticesEFIdentity.Core.Dtos;
using BestPracticesEFIdentity.Core.Entities;
using BestPracticesEFIdentity.Service.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BestPracticesEFIdentity.Service.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<CreateUserResponseDto> CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellation)
    {
        IdentityResult result =  await _userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = createUserDto.Username,
            Email = createUserDto.Email,
            NameSurname = createUserDto.NameSurname
             
        }, createUserDto.Password);

        CreateUserResponseDto responseDto = new CreateUserResponseDto() { Succeeded = result.Succeeded };

        if (result.Succeeded) responseDto.Message = "Kayıt Başarılı";
        else
        {
            foreach (var error in result.Errors)
            {
                responseDto.Message += error.Description;
            }
        }
        return responseDto;

    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto, CancellationToken cancellation)
    {
        AppUser appUser =  await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
        if (appUser == null) appUser = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);

        if (appUser == null)
        {
            throw new Exception("Kullanıcı veya şifre hatalı...");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginDto.Password, false);

        if (result.Succeeded) {

            TokenDto tokenDto = _tokenService.CreateAccessToken(15);
            await UpdateRefreshToken(tokenDto.RefreshToken, appUser, tokenDto.Expiration, 15);
            return new()
            {
                Token = tokenDto,
                Message = "Giriş Başarılı"
            };
        }

        return new() { Message = "Başarısız Giriş Denemesi!" };

    }

    public async Task<TokenDto> RenewRefreshToken(string refreshToken, CancellationToken cancellation)
    {
        AppUser? appUser =  await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        if (appUser != null && appUser?.RefreshTokenExpiration > DateTime.UtcNow)
        {
            TokenDto token = _tokenService.CreateAccessToken(15);
            await UpdateRefreshToken(token.RefreshToken, appUser, token.Expiration, 15);
            return token;
        }
        else
        {
            throw new Exception("Not Found User!");
        }
    }

    public async Task UpdateRefreshToken(string refreshToken, AppUser appUser, DateTime accessTokenDate, int addOnAccessTokenDate)
    {
        if (appUser != null) {

            appUser.RefreshToken = refreshToken;
            appUser.RefreshTokenExpiration = accessTokenDate.AddSeconds(addOnAccessTokenDate);
            await _userManager.UpdateAsync(appUser);
        }
        else
        {
            throw new Exception("Refresh Token oluştururken hata oluştur");
        }
    }


}
