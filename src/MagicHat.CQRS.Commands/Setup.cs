using MagicHat.CQRS.Commands.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace MagicHat.CQRS.Commands;

public static class Setup
{
    public static IMagicHatConfigurator AddCommands(this IMagicHatConfigurator configurator)
    {
        configurator.Services.AddSingleton<ICommandDispatcher, ServiceLocatorCommandDispatcher>();
        return configurator;
    } 
}
