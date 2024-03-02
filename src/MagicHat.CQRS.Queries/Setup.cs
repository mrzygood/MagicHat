using ClassLibrary1MagicHat.CQRS.Queries.Dispatchers;
using MagicHat;
using Microsoft.Extensions.DependencyInjection;

namespace ClassLibrary1MagicHat.CQRS.Queries;

public static class Setup
{
    public static IMagicHatConfigurator AddQueries(this IMagicHatConfigurator configurator)
    {
        configurator.Services.AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();
        return configurator;
    } 
}
