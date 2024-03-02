using MagicHat.Exceptions;
using MagicHat.Vault.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VaultSharp.Extensions.Configuration;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.AuthMethods.UserPass;

namespace MagicHat.Vault;

public static class Setup
{
    public static IMagicHatConfigurator AddVault(
        this IMagicHatConfigurator configurator,
        IConfigurationBuilder configurationBuilder,
        string configurationSectionName = "Vault")
    {
        if (string.IsNullOrWhiteSpace(configurationSectionName))
        {
            throw new MissingConfigurationSectionException("Vault", configurationSectionName);
        }
        var configuration = configurator.GetSection<VaultConfiguration>(configurationSectionName);

        configurator.Services.AddSingleton(configuration);

        if (configuration.LoadConfiguration)
        {
            AbstractAuthMethodInfo authMethod;
            if (string.IsNullOrWhiteSpace(configuration.Token))
            {
                authMethod = new UserPassAuthMethodInfo(configuration.UserName, configuration.Password);
            }
            else
            {
                authMethod = new TokenAuthMethodInfo(configuration.Token);
            }
            
            configurationBuilder.AddVaultConfiguration(
                () => new VaultOptions(
                    configuration.Url,
                    authMethod),
                configuration.BasePath,
                mountPoint: configuration.MountPoint
            );
        }
        
        return configurator;
    }
}
