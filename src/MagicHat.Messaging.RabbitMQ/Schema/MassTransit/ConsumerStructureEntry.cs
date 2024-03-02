namespace MagicHat.Messaging.RabbitMQ.Schema.MassTransit;

public sealed record ConsumerStructureEntry(string ConsumerClassName, ICollection<Type> Messages);
