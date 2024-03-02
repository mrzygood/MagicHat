namespace MagicHat.CQRS.Commands;

// TODO `TCommand` have to be class?
public interface ICommandBus
{
    Task Send<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : class, ICommand;
}
