using Microsoft.Extensions.DependencyInjection;

namespace MagicHat.CQRS.Commands.Dispatchers;

internal sealed class ServiceLocatorCommandDispatcher : ICommandDispatcher
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ServiceLocatorCommandDispatcher(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    public async Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class, ICommand
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var commandHandler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await commandHandler.HandleAsync(command, cancellationToken);
    }
}
