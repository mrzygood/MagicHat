namespace MagicHat.Messaging.RabbitMQ.Consuming;

public interface IMessageHandler<in TMessage> where TMessage : class
{
    Task Handle(TMessage message);
}
