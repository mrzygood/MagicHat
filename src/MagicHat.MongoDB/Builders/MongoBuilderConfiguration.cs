namespace MagicHat.MongoDB.Builders;

internal class MongoBuilderConfiguration
{
    private readonly IDictionary<string, MongoCollectionEntry> _schemaConfiguration;
    
    public IReadOnlyDictionary<string, MongoCollectionEntry> SchemaConfiguration 
        => (IReadOnlyDictionary<string, MongoCollectionEntry>)_schemaConfiguration;
    public bool AutoSchemaCreationEnabled { get; init; }

    public MongoBuilderConfiguration(
        IDictionary<string, MongoCollectionEntry> schemaConfiguration,
        bool isAutoSchemaCreationEnabled)
    {
        _schemaConfiguration = schemaConfiguration;
        AutoSchemaCreationEnabled = isAutoSchemaCreationEnabled;
    }
}
