using System.Reflection;

namespace MagicHat.Messaging.RabbitMQ.Schema.MassTransit;

internal sealed class MassTransitSchemaProvider : ISchemaProvider
{
    private readonly ISchemaNamingPolicy _schemaNamingPolicy;

    public MassTransitSchemaProvider(ISchemaNamingPolicy schemaNamingPolicy)
    {
        _schemaNamingPolicy = schemaNamingPolicy;
    }
    
    public Task<SchemaConfiguration> GetSchema()
    {
        var consumersWithRelatedMessages = FetchConsumersWithRelatedMessages();
        var schemaConfiguration = new SchemaConfiguration
        {
            ExchangesBindings = new Dictionary<string, ICollection<string>>(),
            Exchanges = consumersWithRelatedMessages
                .Select(x => _schemaNamingPolicy.Apply(x.ConsumerClassName))
                .ToList(),
            Queues = consumersWithRelatedMessages
                .SelectMany(x => x.Messages)
                .Select(x => _schemaNamingPolicy.Apply(x.Name))
                .ToList(),
        };
        
        foreach (var consumersWithRelatedMessage in consumersWithRelatedMessages)
        {
            schemaConfiguration.ExchangeQueueBindings = new Dictionary<string, ICollection<string>>();
            var exchangeName = _schemaNamingPolicy.Apply(consumersWithRelatedMessage.ConsumerClassName);
            var queueNames = consumersWithRelatedMessage
                .Messages
                .Select(x => _schemaNamingPolicy.Apply(x.Name))
                .ToList();
            schemaConfiguration.ExchangeQueueBindings[exchangeName] = queueNames;
        }
        
        return Task.FromResult(schemaConfiguration);
    }
    
    private static IList<ConsumerStructureEntry> FetchConsumersWithRelatedMessages()
    {
        var consumerInterface = typeof(IConsumer<>);

        var consumerTypes = GetTypesImplementingGenericInterface(GetAssemblies(), consumerInterface)
            .Select(x => new ConsumerStructureEntry(
                x.Name,
                GetInterfaceGenericArgumentsForClass(x, consumerInterface)))
            .ToList();

        return consumerTypes;
    }

    // TODO user have to specify assemblies as params
    // TODO or get all defined
    private static IEnumerable<Assembly> GetAssemblies()
    {
        var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Where(x => x.Contains("Ecommerce."))
            .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));
        
        // Get all loaded assemblies
        var loadedAssemblies = assemblies
            .Where(x => x.GetName().Name != null && x.GetName().Name!.Contains("Ecommerce."));

        return loadedAssemblies;
    }
    
    private static IEnumerable<Type> GetTypesImplementingGenericInterface(
        IEnumerable<Assembly> assemblies,
        Type genericInterfaceType)
    {
        return assemblies
            .SelectMany(s => s.GetTypes())
            .Where(x => x.GetInterfaces()
                .Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == genericInterfaceType));
    }
    
    private static ICollection<Type> GetInterfaceGenericArgumentsForClass(
        Type classImplementingInterface,
        Type genericInterfaceType)
    {
        return classImplementingInterface
            .GetInterfaces()
            .Where(y => y.IsGenericType && y.GetGenericTypeDefinition() == genericInterfaceType)
            .Select(i => i.GetGenericArguments()[0])
            .ToList();
    }
}
