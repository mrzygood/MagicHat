namespace MagicHat.MongoDB;

public interface ICrudRepository<TDocument>
{
    Task<TDocument> Get(Guid id, CancellationToken ct = default);
    Task<TDocument?> Find(Guid id, CancellationToken ct = default);
    Task Add(TDocument document, CancellationToken ct = default);
    Task Add(ICollection<TDocument> documents, CancellationToken ct = default);
    Task Update(TDocument document, CancellationToken ct = default);
    Task Delete(Guid id, CancellationToken ct = default);
}
