using ClassLibrary1MagicHat.CQRS.Queries;

namespace OnlineShop.CQRS.Queries;

public sealed record FirstQuery(string Param1) : IQuery<FirstResultDto>;
