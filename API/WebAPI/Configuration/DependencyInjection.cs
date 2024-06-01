using Licenta.Services;
using Services.Authentication;
using Services.Common.ApplicationConfigOptions;

namespace WebAPI.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddHostedService<BidMonitoringService>();
        services.AddHttpContextAccessor();
        services.AddScoped<IAuthTokenService, AuthTokenService>();
        services.AddAuthentication().AddJwtBearer();
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        return services;   
    }
}
