using System.Text;
using MagicHat.Messaging.RabbitMQ.Connection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MagicHat.Messaging.RabbitMQ.Consuming;

public interface IMessageConsumer
{
    Task Consume<T>(
        string queue,
        string routingKey,
        string exchange,
        Func<T, IBasicProperties, Task> handler) where T : class;
}

internal class MessageConsumer : IMessageConsumer
{
    private readonly IChannelFactory _channelFactory;

    public MessageConsumer(IChannelFactory channelFactory)
    {
        _channelFactory = channelFactory;
    }
    
    public Task Consume<T>(
        string queue,
        string routingKey,
        string exchange,
        Func<T, IBasicProperties, Task> handler) where T : class
    {
        var channel = _channelFactory.Create(); // What about disposing
        channel.QueueDeclare(queue, durable: false, exclusive: false, autoDelete: false);
        channel.QueueBind(queue, exchange, routingKey);
        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (_, ea) =>
        {
            var body = ea.Body.ToArray();
            var messageSerialized = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Message form queue '{queue}' consumed" +
                              $"by thread '{Thread.CurrentThread.ManagedThreadId}' ({Thread.CurrentThread.IsThreadPoolThread}). " +
                              $"Payload: {messageSerialized}");
            var message = JsonConvert.DeserializeObject<T>(messageSerialized); // TODO add possible to replace serializer
            await handler(message!, ea.BasicProperties); // TODO handle invalid type serialized
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };
        
        channel.BasicConsume(queue: queue,
            autoAck: false,
            consumer: consumer);
        
        return Task.CompletedTask;
    }
}
