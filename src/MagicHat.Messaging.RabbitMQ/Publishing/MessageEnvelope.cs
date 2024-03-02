namespace MagicHat.Messaging.RabbitMQ.Publishing;

public sealed class MessageEnvelope<TMessage> where TMessage : class
{
    public MessageEnvelope(TMessage message)
    {
        Message = message;
    }

    public TMessage Message { get; set; }
}
