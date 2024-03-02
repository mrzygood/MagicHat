using System.Net.Http.Json;
using System.Text.Json;

namespace MagicHat.ServiceDiscovery.Consul.HttpClients;

public sealed class HttpClientBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IServiceDiscoveryAccessor _serviceDiscoveryAccessor;

    public HttpClientBase(IHttpClientFactory httpClientFactory, IServiceDiscoveryAccessor serviceDiscoveryAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _serviceDiscoveryAccessor = serviceDiscoveryAccessor;
    }
    
    public async Task<T?> Get<T>(string serviceName, string queryPath, CancellationToken ct = default)
    {
        var serviceUrl = await _serviceDiscoveryAccessor.GetServiceUrl(serviceName, ct);
        using var httpClient = _httpClientFactory.CreateClient();
        T? result = await httpClient.GetFromJsonAsync<T>(
            "http://" + serviceUrl + $"/{queryPath}",
            new JsonSerializerOptions(JsonSerializerDefaults.Web), ct);

        return result;
    }
}
