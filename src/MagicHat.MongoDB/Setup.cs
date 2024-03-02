using MagicHat.MongoDB.Builders;
using MagicHat.MongoDB.Configuration;
using MagicHat.MongoDB.Exceptions;
using MagicHat.MongoDB.Initializers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MagicHat.MongoDB;

public static class Setup
{
    public static IMagicHatConfigurator AddMongoDb(
        this IMagicHatConfigurator configurator,
        Action<IMongoDbBuilder> mongoBuilder,
        string configurationSectionName = "mongo")
    {
        if (string.IsNullOrWhiteSpace(configurationSectionName))
        {
            throw new MongoDbMissingConfigurationException();
        }
        var mongoConfiguration = configurator.GetSection<MongoConfiguration>(configurationSectionName);
        return configurator.AddMongoDb(mongoBuilder, mongoConfiguration);
    }

    public static IMagicHatConfigurator AddMongoDb(
        this IMagicHatConfigurator configurator,
        Action<IMongoDbBuilder> mongoBuilder,
        Action<MongoConfiguration> configureMongo)
    {
        var mongoConfiguration = new MongoConfiguration();
        configureMongo.Invoke(mongoConfiguration);

        return configurator.AddMongoDb(mongoBuilder, mongoConfiguration);
    }

    public static IMagicHatConfigurator AddMongoDb(
        this IMagicHatConfigurator configurator,
        Action<IMongoDbBuilder> mongoBuilder,
        MongoConfiguration configureMongo)
    {
        configurator.Services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = MongoClientSettings.FromConnectionString(configureMongo.Url);
            settings.ConnectTimeout = TimeSpan.FromMilliseconds(configureMongo.ConnectTimeout);
            settings.SocketTimeout = TimeSpan.FromMilliseconds(configureMongo.SocketTimeout);

            return new MongoClient(settings);
        });
        configurator.Services.AddSingleton<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(configureMongo.Database);
        });

        var builder = new MongoDbBuilder(configurator);
        mongoBuilder.Invoke(builder);
        
        configurator.Services.AddSingleton<CollectionNamesAccessor>();
        configurator.Services.AddHostedService<MongoInitializer>();
        return configurator;
    }
}
