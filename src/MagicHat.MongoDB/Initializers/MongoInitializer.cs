using System.Collections.Immutable;
using MagicHat.MongoDB.Builders;
using MagicHat.MongoDB.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MagicHat.MongoDB.Initializers;

internal sealed class MongoInitializer : IHostedService
{
    private readonly IMongoDatabase _mongoDatabase;
    private readonly MongoConfiguration _mongoDbSettings;
    private readonly MongoBuilderConfiguration _mongoBuilderConfiguration;

    public MongoInitializer(
        IOptions<MongoConfiguration> mongoDbSettings,
        IMongoDatabase mongoDatabase,
        MongoBuilderConfiguration mongoBuilderConfiguration)
    {
        _mongoDatabase = mongoDatabase;
        _mongoBuilderConfiguration = mongoBuilderConfiguration;
        _mongoDbSettings = mongoDbSettings.Value;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (_mongoBuilderConfiguration.AutoSchemaCreationEnabled)
        {
            await CreateCollections(
                _mongoBuilderConfiguration.SchemaConfiguration.Keys.ToImmutableList(),
                cancellationToken);
        }
    }

    private async Task CreateCollections(
        IReadOnlyCollection<string> collectionsExpectedToBeCreated,
        CancellationToken cancellationToken)
    {
        var collectionNamesResult = await _mongoDatabase.ListCollectionNamesAsync(cancellationToken: cancellationToken);
        var existingCollectionNames = await collectionNamesResult.ToListAsync(cancellationToken: cancellationToken);
        foreach (var collectionNameToBeCreated in collectionsExpectedToBeCreated)
        {
            if (existingCollectionNames.All(x => x != collectionNameToBeCreated))
            {
                await _mongoDatabase.CreateCollectionAsync(collectionNameToBeCreated, cancellationToken: cancellationToken);
            }
            
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
