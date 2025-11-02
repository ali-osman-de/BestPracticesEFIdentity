using BestPracticesEFIdentity.Core.Dtos;

namespace BestPracticesEFIdentity.Service.Interfaces.Services;

public interface IUserService
{
    Task<CreateUserResponseDto> CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellation);
    Task<LoginResponseDto> LoginAsync(LoginDto loginDto, CancellationToken cancellation);
}
