using Microsoft.Extensions.DependencyInjection;
using Pokemon.Application.Services;
using Pokemon.Infraestructure.Services;

namespace Pokemon.Infraestructure;

public static class Startup
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services)
    {
        services.AddScoped<IPokeApiService, PokeApiService>();
        services.AddScoped<IMasterService, MasterService>();
        services.AddScoped<ICaptureService, CaptureService>();
        services.AddHttpClient<IPokeApiService, PokeApiService>();

        return services;


    }
}
