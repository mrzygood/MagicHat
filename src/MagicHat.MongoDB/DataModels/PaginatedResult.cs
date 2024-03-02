namespace MagicHat.MongoDB.DataModels;

public sealed record PaginatedResult<T>(
    ICollection<T> Results,
    int PageIndex,
    int PageSize,
    int Total);
