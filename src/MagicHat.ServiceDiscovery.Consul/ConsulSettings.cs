namespace MagicHat.ServiceDiscovery.Consul;

internal sealed class ConsulSettings
{
    public string Url { get; set; }
    public string ServiceAddress { get; set; }
    public string ServiceName { get; set; }
    public int ServicePort { get; set; }
    public ConsulHealthCheck? HealthCheck { get; set; }
}

internal sealed class ConsulHealthCheck
{
    public string Url { get; set; }
    public int Interval { get; set; }
    public int Timeout { get; set; }
}
