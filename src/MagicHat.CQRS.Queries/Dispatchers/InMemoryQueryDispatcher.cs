using Microsoft.Extensions.DependencyInjection;

namespace ClassLibrary1MagicHat.CQRS.Queries.Dispatchers;

internal sealed class InMemoryQueryDispatcher : IQueryDispatcher
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public InMemoryQueryDispatcher(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    public async Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var unboundedQueryHandlerType = typeof(IQueryHandler<,>);
        var queryHandlerBoundedType = unboundedQueryHandlerType.MakeGenericType(query.GetType(), typeof(TResult));
        var commandHandler = scope.ServiceProvider.GetRequiredService(queryHandlerBoundedType);
        
        var method = queryHandlerBoundedType.GetMethod("HandleAsync");
        if (method is null)
        {
            throw new InvalidOperationException($"Query handler for '{query.GetType().Name}' is invalid");
        }

        return await (Task<TResult>) method.Invoke(commandHandler, new object[] { query, cancellationToken })!;
    }
}
