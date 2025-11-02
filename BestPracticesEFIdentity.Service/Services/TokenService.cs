using BestPracticesEFIdentity.Core.Dtos;
using BestPracticesEFIdentity.Service.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BestPracticesEFIdentity.Service.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenDto CreateAccessToken(int minutes)
    {
        TokenDto tokenDto = new TokenDto();

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]!));

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        tokenDto.Expiration = DateTime.UtcNow.AddMinutes(minutes);
        JwtSecurityToken jwtSecurityToken = new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: tokenDto.Expiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials
        );

        JwtSecurityTokenHandler securityTokenHandler = new();
        tokenDto.AccessToken = securityTokenHandler.WriteToken(jwtSecurityToken);

        return tokenDto;
    }
}
