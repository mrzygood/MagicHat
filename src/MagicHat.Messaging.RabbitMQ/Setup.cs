using MagicHat.Messaging.RabbitMQ.Connection;
using MagicHat.Messaging.RabbitMQ.Consuming;
using MagicHat.Messaging.RabbitMQ.Publishing;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace MagicHat.Messaging.RabbitMQ;

public static class Setup
{
    public static IMagicHatConfigurator AddRabbitMq(
        this IMagicHatConfigurator configurator,
        RabbitMqConfiguration configuration)
    {
        var factory = new ConnectionFactory
        {
            HostName = configuration.Host,
            Port = configuration.Port,
            VirtualHost = configuration.VirtualHost,
            UserName = configuration.Username,
            Password = configuration.Password,
            ClientProvidedName = configuration.ClientProvidedName,
        };
        var connection = factory.CreateConnection();
        
        configurator.Services.AddSingleton(connection);
        
        configurator.Services.AddSingleton<IMessagePublisher, MessagePublisher>();
        configurator.Services.AddSingleton<ChannelAccessor>();
        configurator.Services.AddSingleton<IChannelFactory, ChannelFactory>();
        
        configurator.AddConsumers();
        
        return configurator;
    }
}
