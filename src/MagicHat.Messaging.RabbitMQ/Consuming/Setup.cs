using Microsoft.Extensions.DependencyInjection;

namespace MagicHat.Messaging.RabbitMQ.Consuming;

public static class Setup
{
    public static IMagicHatConfigurator AddConsumers(this IMagicHatConfigurator configurator)
    {
        configurator.Services.AddSingleton<IMessageConsumer, MessageConsumer>();
        configurator.Services.AddSingleton<IMessageDispatcher, MessageDispatcher>();
        
        return configurator;
    }
}
