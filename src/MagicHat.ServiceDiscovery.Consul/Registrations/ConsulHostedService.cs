using Consul;
using Microsoft.Extensions.Hosting;

namespace MagicHat.ServiceDiscovery.Consul.Registrations;

internal sealed class ConsulHostedService : IHostedService
{
    private readonly IConsulClient _consulClient;
    private readonly ConsulSettings _consulSettings;
    private string? _serviceId;

    public ConsulHostedService(IConsulClient consulClient, ConsulSettings consulSettings)
    {
        _consulClient = consulClient;
        _consulSettings = consulSettings;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _serviceId = $"{_consulSettings.ServiceName}_{_consulSettings.ServicePort}";
        var registration = new AgentServiceRegistration
        {
            ID = _serviceId,
            Name = _consulSettings.ServiceName,
            Address = _consulSettings.ServiceAddress,
            Port = _consulSettings.ServicePort
        };

        if (_consulSettings.HealthCheck is not null)
        {
            registration.Check = new AgentServiceCheck
            {
                HTTP = _consulSettings.HealthCheck.Url,
                Interval = TimeSpan.FromSeconds(_consulSettings.HealthCheck.Interval),
                Timeout = TimeSpan.FromSeconds(_consulSettings.HealthCheck.Timeout),
                TLSSkipVerify = true,
                DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
            };
        }
        
        await _consulClient.Agent.ServiceDeregister(_consulSettings.ServiceName, cancellationToken);
        await _consulClient.Agent.ServiceRegister(registration, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _consulClient.Agent.ServiceDeregister(_serviceId, cancellationToken);
    }
}
