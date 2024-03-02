namespace MagicHat.Messaging.RabbitMQ;

public interface IMessagePublisher
{
    Task PublishAsync<TMessage>(
        TMessage message,
        string exchange,
        string routingKey) where TMessage : class;
}
