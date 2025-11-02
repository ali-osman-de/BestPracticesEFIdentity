using BestPracticesEFIdentity.Core.Dtos;
using BestPracticesEFIdentity.Core.Entities;
using BestPracticesEFIdentity.Service.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

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

            TokenDto tokenDto = _tokenService.CreateAccessToken(5);
            return new()
            {
                Token = tokenDto,
                Message = "Giriş Başarılı"
            };
        }

        return new() { Message = "Başarısız Giriş Denemesi!" };

    }
}
