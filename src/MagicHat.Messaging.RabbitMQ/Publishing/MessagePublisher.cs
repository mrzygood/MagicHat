using System.Text;
using System.Text.Json;
using MagicHat.Messaging.RabbitMQ.Connection;
using RabbitMQ.Client;

namespace MagicHat.Messaging.RabbitMQ.Publishing;

internal class MessagePublisher : IMessagePublisher
{
    private readonly IModel _channel;

    public MessagePublisher(IChannelFactory channelFactory)
    {
        _channel = channelFactory.Create();
    }
    
    public Task PublishAsync<TMessage>(TMessage message, string exchange, string routingKey) where TMessage : class
    {
        var messageEnvelope = new MessageEnvelope<TMessage>(message);
        
        var messageProperties = _channel.CreateBasicProperties();

        var messageJson = JsonSerializer.Serialize(messageEnvelope);
        var body = Encoding.UTF8.GetBytes(messageJson);
        
        _channel.BasicPublish(
            exchange,
            routingKey: routingKey,
            basicProperties: messageProperties,
            body: body);

        return Task.CompletedTask;
    }
}
