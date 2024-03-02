using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MagicHat;

public static class Setup
{
    public static IServiceCollection AddMagicHat(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<IMagicHatConfigurator> magicHatConfiguration)
    {
        var builder = new MagicHatConfigurator(services, configuration);
        magicHatConfiguration(builder);

        return services;
    }
    
    public static IServiceCollection UseMagicHat(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<IMagicHatConfigurator> magicHatConfiguration)
    {
        return services;
    }
}
