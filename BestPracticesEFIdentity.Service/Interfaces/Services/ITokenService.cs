using BestPracticesEFIdentity.Core.Dtos;

namespace BestPracticesEFIdentity.Service.Interfaces.Services;

public interface ITokenService
{
    TokenDto CreateAccessToken(int minutes);
    string CreateRefreshToken();
}
