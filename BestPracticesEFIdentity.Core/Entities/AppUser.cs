using Microsoft.AspNetCore.Identity;

namespace BestPracticesEFIdentity.Core.Entities;

public class AppUser : IdentityUser<string>
{
    public string NameSurname { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
}
