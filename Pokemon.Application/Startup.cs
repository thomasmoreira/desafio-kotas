using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pokemon.Application.Validators;
using System.Reflection;

namespace Pokemon.Application;

public static class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
        }).AddValidatorsFromAssemblyContaining<PokemonMasterRequestValidator>();


    }
}
