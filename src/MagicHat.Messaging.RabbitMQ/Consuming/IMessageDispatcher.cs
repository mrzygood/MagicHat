using Microsoft.Extensions.DependencyInjection;

namespace MagicHat.Messaging.RabbitMQ.Consuming;

public interface IMessageDispatcher
{
    Task Dispatch<T>(T message) where T : class;
}

internal class MessageDispatcher : IMessageDispatcher
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public MessageDispatcher(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    public async Task Dispatch<T>(T message) where T : class
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IMessageHandler<T>>(); // TODO how to register it in IoC
        await handler.Handle(message);
    }
}
