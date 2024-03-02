using ClassLibrary1MagicHat.CQRS.Queries;

namespace OnlineShop.CQRS.Queries;

public sealed class FirstQueryHandler : IQueryHandler<FirstQuery, FirstResultDto>
{
    public Task<FirstResultDto> HandleAsync(FirstQuery query, CancellationToken cancellationToken)
    {
        return Task.FromResult(new FirstResultDto("FakeValue"));
    }
}
