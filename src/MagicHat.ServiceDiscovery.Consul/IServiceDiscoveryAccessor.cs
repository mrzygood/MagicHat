using Consul;

namespace MagicHat.ServiceDiscovery.Consul;

public interface IServiceDiscoveryAccessor
{
    Task<Uri?> GetServiceUrl(string serviceName, CancellationToken ct = default);
}

internal class ServiceDiscoveryAccessor : IServiceDiscoveryAccessor
{
    private readonly IConsulClient _consulClient;

    public ServiceDiscoveryAccessor(IConsulClient consulClient)
    {
        _consulClient = consulClient;
    }

    public async Task<Uri?> GetServiceUrl(string serviceName, CancellationToken ct = default)
    {
        var servicesResult = await _consulClient.Catalog.Service(serviceName, ct);
        var urlAndPort = servicesResult
            .Response
            .Select(x => new Tuple<string, int>(x.ServiceAddress, x.ServicePort))
            .FirstOrDefault();

        if (urlAndPort is not null)
        {
            return new Uri(urlAndPort.Item1 + ":" + urlAndPort.Item2);
        }

        return null;
    }
}
