using BestPracticesEFIdentity.Service.Interfaces.Services;
using BestPracticesEFIdentity.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BestPracticesEFIdentity.Service.Extensions;

public static class ServiceExtensions
{

    public static void AddServiceLayerServiceCongf(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
    }

}
