using BestPracticesEFIdentity.Core.Dtos;
using BestPracticesEFIdentity.Core.Entities;

namespace BestPracticesEFIdentity.Service.Interfaces.Services;

public interface IUserService
{
    Task<CreateUserResponseDto> CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellation);
    Task<LoginResponseDto> LoginAsync(LoginDto loginDto, CancellationToken cancellation);
    Task UpdateRefreshToken(string refreshToken, AppUser appUser, DateTime accessTokenDate, int addOnAccessTokenDate);
    Task<TokenDto> RenewRefreshToken(string refreshToken, CancellationToken cancellation);
}
