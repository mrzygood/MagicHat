namespace MagicHat.Messaging.RabbitMQ.Schema;

internal interface ISchemaProvider
{
    Task<SchemaConfiguration> GetSchema();
}
