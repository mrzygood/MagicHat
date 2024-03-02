using Consul;
using MagicHat.ServiceDiscovery.Consul.Registrations;
using Microsoft.Extensions.DependencyInjection;

namespace MagicHat.ServiceDiscovery.Consul;

public static class Setup
{
    public static IMagicHatConfigurator AddConsul(
        this IMagicHatConfigurator configurator,
        string configurationSectionName = "consul")
    {
        if (string.IsNullOrWhiteSpace(configurationSectionName))
        {
            throw new ConsulConfigurationException();
        }

        var consulConfiguration = configurator.GetSection<ConsulSettings>(configurationSectionName);
        configurator.Services.AddSingleton(consulConfiguration);
        
        configurator.Services.AddSingleton<IConsulClient, ConsulClient>(_ => new ConsulClient(consulConfig =>
        {
            consulConfig.Address = new Uri(consulConfiguration.Url);
        }));

        configurator.Services.AddHostedService<ConsulHostedService>();
        configurator.Services.AddSingleton<IServiceDiscoveryAccessor, ServiceDiscoveryAccessor>();
        configurator.Services.AddSingleton<IServiceDiscoveryAccessor, ServiceDiscoveryAccessor>();

        return configurator;
    }
}
