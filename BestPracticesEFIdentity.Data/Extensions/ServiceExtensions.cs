using BestPracticesEFIdentity.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BestPracticesEFIdentity.Data.Extensions;

public static class ServiceExtensions
{

    public static void AddDataLayerServiceCongf(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddIdentityCore<AppUser>().AddRoles<AppRole>()
                                           .AddSignInManager<SignInManager<AppUser>>()
                                           .AddEntityFrameworkStores<AppDbContext>();

        services.AddSingleton<TimeProvider>(TimeProvider.System);
    }

}
