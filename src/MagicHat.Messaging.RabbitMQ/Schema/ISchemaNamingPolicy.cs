namespace MagicHat.Messaging.RabbitMQ.Schema;

public interface ISchemaNamingPolicy
{
    string Apply(string name);
}
