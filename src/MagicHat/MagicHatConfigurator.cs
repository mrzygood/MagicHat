using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MagicHat;

internal sealed class MagicHatConfigurator : IMagicHatConfigurator
{
    private readonly IConfiguration _configuration;
    public IServiceCollection Services { get; }
    
    public MagicHatConfigurator(IServiceCollection services, IConfiguration configuration)
    {
        Services = services;
        _configuration = configuration;
    }
    
    public T GetSection<T>(string sectionName) where T : class, new()
    {
        var section = _configuration.GetRequiredSection(sectionName);
        var model = new T();
        section.Bind(model);

        return model;
    }
}
