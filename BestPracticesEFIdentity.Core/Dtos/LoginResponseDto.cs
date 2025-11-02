namespace BestPracticesEFIdentity.Core.Dtos;

public class LoginResponseDto
{
    public TokenDto Token { get; set; }
    public string Message { get; set; }
}
