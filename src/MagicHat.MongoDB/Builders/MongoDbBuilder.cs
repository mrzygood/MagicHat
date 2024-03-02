using Humanizer;
using MagicHat.MongoDB.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MagicHat.MongoDB.Builders;

internal sealed class MongoDbBuilder : IMongoDbBuilder
{
    private bool _isBuilt = false;
    private bool _autoSchemaCreationEnabled;
    private readonly IDictionary<string, MongoCollectionEntry> _collections = new Dictionary<string, MongoCollectionEntry>();
    public IMagicHatConfigurator MagicHatConfigurator { get; }
    
    public MongoDbBuilder(IMagicHatConfigurator magicHatConfigurator)
    {
        MagicHatConfigurator = magicHatConfigurator;
    }

    public IMongoDbBuilder WithCollection<T>(string? collectionName = null, bool withRepository = false) where T : IIdentifiable
    {
        AlreadyBuiltGuard();
        _collections.Add(collectionName ?? typeof(T).Name.Kebaberize(), new MongoCollectionEntry(typeof(T), withRepository));
        if (withRepository)
        {
            MagicHatConfigurator.Services.AddScoped<ICrudRepository<T>, CrudRepository<T>>();
        }
        return this;
    }

    public IMongoDbBuilder AddRepository<T>() where T : IIdentifiable
    {
        var existingCollectionEntryKey = _collections
            .FirstOrDefault(x => x.Value.DocumentType == typeof(T))
            .Key;

        if (existingCollectionEntryKey is not null && _collections[existingCollectionEntryKey].HasRepository)
        {
            return this;
        }
        
        MagicHatConfigurator.Services.AddScoped<ICrudRepository<T>, CrudRepository<T>>();
        
        if (existingCollectionEntryKey is null)
        {
            _collections.Add(typeof(T).Name.Kebaberize(), new MongoCollectionEntry(typeof(T), true));
            return this;
        }
        
        var existingCollectionEntry = _collections[existingCollectionEntryKey];
        _collections[existingCollectionEntryKey] = existingCollectionEntry with { HasRepository = true };
        
        return this;
    }

    public IMongoDbBuilder CreateOnStartup()
    {
        AlreadyBuiltGuard();
        _autoSchemaCreationEnabled = true;
        return this;
    }

    public IMagicHatConfigurator Build()
    {
        AlreadyBuiltGuard();
        MagicHatConfigurator.Services.AddSingleton(new MongoBuilderConfiguration(_collections, _autoSchemaCreationEnabled));
        _isBuilt = true;
        return MagicHatConfigurator;
    }

    private void AlreadyBuiltGuard()
    {
        if (_isBuilt)
        {
            throw new InvalidOperationException("Configuration is already built");
        } 
    }
}
