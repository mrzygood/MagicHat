namespace MagicHat.Messaging.RabbitMQ.Schema.MassTransit;

// TODO contravariant means
public interface IConsumer<in TMessage> where TMessage : class
{
    Task Consume(TMessage message);
}
