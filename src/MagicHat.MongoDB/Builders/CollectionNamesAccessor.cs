using System.Collections.Concurrent;

namespace MagicHat.MongoDB.Builders;

internal sealed class CollectionNamesAccessor
{
    private readonly ConcurrentDictionary<string, string> _documentCollectionNames = new ();

    public CollectionNamesAccessor(MongoBuilderConfiguration mongoBuilderConfiguration)
    {
        foreach (var mongoCollectionEntry in mongoBuilderConfiguration.SchemaConfiguration)
        {
            _documentCollectionNames.TryAdd(mongoCollectionEntry.Value.DocumentType.FullName!, mongoCollectionEntry.Key);
        }
    }

    public string GetCollectionName(Type documentType) => _documentCollectionNames[documentType.FullName!];
}
