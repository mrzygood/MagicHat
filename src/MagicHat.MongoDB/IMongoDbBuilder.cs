namespace MagicHat.MongoDB;

public interface IMongoDbBuilder
{
    IMagicHatConfigurator MagicHatConfigurator { get; }
    IMongoDbBuilder WithCollection<T>(string? collectionName = null, bool withRepository = false) where T : IIdentifiable;
    IMongoDbBuilder AddRepository<T>() where T : IIdentifiable;
    IMongoDbBuilder CreateOnStartup();
    IMagicHatConfigurator Build();
}
