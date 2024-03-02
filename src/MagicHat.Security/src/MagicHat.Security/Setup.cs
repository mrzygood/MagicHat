using System.Runtime.CompilerServices;
using MagicHat.Security.Signing;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("MagicHat.SecurityTests")]
namespace MagicHat.Security;

public static class Setup
{
    public static IMagicHatConfigurator AddSecurity(
        this IMagicHatConfigurator configurator,
        string configurationSectionName = "Security")
    {
        if (string.IsNullOrWhiteSpace(configurationSectionName))
        {
            throw new SecurityMissingConfigurationException();
        }
        var mongoConfiguration = configurator.GetSection<SecurityConfiguration>(configurationSectionName);
        return configurator.AddSecurity(mongoConfiguration);
    }

    public static IMagicHatConfigurator AddSecurity(
        this IMagicHatConfigurator configurator,
        Action<SecurityConfiguration> configuration)
    {
        var mongoConfiguration = new SecurityConfiguration();
        configuration.Invoke(mongoConfiguration);

        return configurator.AddSecurity(mongoConfiguration);
    }

    public static IMagicHatConfigurator AddSecurity(
        this IMagicHatConfigurator configurator,
        SecurityConfiguration configuration)
    {
        configurator.Services.AddSingleton<ISigner, Signer>();
        return configurator;
    }
}
