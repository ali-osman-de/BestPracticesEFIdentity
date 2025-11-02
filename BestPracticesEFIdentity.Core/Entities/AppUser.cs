using Microsoft.AspNetCore.Identity;

namespace BestPracticesEFIdentity.Core.Entities;

public class AppUser : IdentityUser<string>
{
    public string NameSurname { get; set; }
}
