using RabbitMQ.Client;

namespace MagicHat.Messaging.RabbitMQ.Connection;

internal interface IChannelFactory
{
    IModel Create();
}
