using MagicHat.CQRS.Commands;

namespace OnlineShop.CQRS.Commands;

public sealed class FirstCommandHandler : ICommandHandler<FirstCommand>
{
    private readonly ILogger<FirstCommandHandler> _logger;

    public FirstCommandHandler(ILogger<FirstCommandHandler> logger)
    {
        _logger = logger;
    }
    
    public Task HandleAsync(FirstCommand command, CancellationToken cancellationToken)
    {
        _logger.LogDebug(
            "{FirstCommandHandlerName} executed with value: {CommandParamValue}",
            nameof(FirstCommandHandler),
            command.Value1);
        return Task.CompletedTask;
    }
}
