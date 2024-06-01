using Business.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Services.Authentication;
using Services.CacheService;
using Services.Common.ApplicationConfigOptions;
using Services.Context;

namespace Services.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
        

        services.AddScoped<SqlDataContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ICacheService, CacheService.CacheService>();

        return services;    
    }
}
