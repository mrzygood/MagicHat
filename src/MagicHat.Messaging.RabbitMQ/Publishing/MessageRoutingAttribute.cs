namespace MagicHat.Messaging.RabbitMQ.Publishing;

public sealed class MessageRoutingAttribute : Attribute
{
    string Exchange;

    public MessageRoutingAttribute(string exchange)
    {
        Exchange = exchange;
    }
}
