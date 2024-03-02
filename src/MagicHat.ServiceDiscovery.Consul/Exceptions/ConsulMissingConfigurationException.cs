namespace MagicHat.ServiceDiscovery.Consul.Exceptions;

public sealed class ConsulMissingConfigurationException : MagicHatException
{
    public ConsulMissingConfigurationException() : base("Consul configuration is missing")
    {
    }
}
