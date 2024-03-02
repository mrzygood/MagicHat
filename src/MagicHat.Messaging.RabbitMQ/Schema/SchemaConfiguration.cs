namespace MagicHat.Messaging.RabbitMQ.Schema;

internal sealed class SchemaConfiguration
{
    // public SchemaConfiguration(
    //     ICollection<string> exchanges,
    //     ICollection<string> queues,
    //     Dictionary<string, ICollection<string>> exchangesBindings,
    //     Dictionary<string, ICollection<string>> exchangeQueueBindings)
    // {
    //     Exchanges = exchanges;
    //     Queues = queues;
    //     ExchangesBindings = exchangesBindings;
    //     ExchangeQueueBindings = exchangeQueueBindings;
    // }
    
    public ICollection<string> Exchanges { get; set; }
    public ICollection<string> Queues { get; set; }
    public Dictionary<string, ICollection<string>> ExchangesBindings { get; set; }
    public Dictionary<string, ICollection<string>> ExchangeQueueBindings { get; set; }
}
