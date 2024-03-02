using MagicHat.MongoDB.Builders;
using MongoDB.Driver;

namespace MagicHat.MongoDB.Repositories;

internal sealed class CrudRepository<T> : ICrudRepository<T> where T : IIdentifiable
{
    private readonly IMongoCollection<T> _collection;

    public CrudRepository(IMongoDatabase mongoDatabase, CollectionNamesAccessor collectionNamesAccessor)
    {
        _collection = mongoDatabase.GetCollection<T>(collectionNamesAccessor.GetCollectionName(typeof(T)));
    }

    public async Task<T> Get(Guid id, CancellationToken ct = default)
    {
        return await _collection.Find(x => x.Id == id).SingleAsync(ct);
    }

    public async Task<T?> Find(Guid id, CancellationToken ct = default)
    {
        return await _collection.Find(x => x.Id == id).SingleOrDefaultAsync(ct);
    }

    public async Task Add(T document, CancellationToken ct = default)
    {
        await _collection.InsertOneAsync(document, cancellationToken: ct);
    }

    public async Task Add(ICollection<T> documents, CancellationToken ct = default)
    {
        await _collection.InsertManyAsync(documents, null, ct);
    }

    public async Task Update(T document, CancellationToken ct = default)
    {
        await _collection.ReplaceOneAsync(x => x.Id == document.Id, document, cancellationToken: ct);
    }

    public async Task Delete(Guid id, CancellationToken ct = default)
    {
        await _collection.DeleteOneAsync(x => x.Id == id, ct);
    }
}
