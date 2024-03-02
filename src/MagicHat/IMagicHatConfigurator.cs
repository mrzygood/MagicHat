using Microsoft.Extensions.DependencyInjection;

namespace MagicHat;

public interface IMagicHatConfigurator
{
    IServiceCollection Services { get; }
    T GetSection<T>(string sectionName) where T : class, new();
}
