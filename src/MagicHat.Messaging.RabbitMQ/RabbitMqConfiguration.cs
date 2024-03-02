using System.Reflection;

namespace MagicHat.Messaging.RabbitMQ;

public sealed class RabbitMqConfiguration
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string VirtualHost { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ClientProvidedName { get; set; }
    public ICollection<Assembly> AssembliesToScan { get; set; }
}
