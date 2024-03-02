namespace MagicHat.MongoDB.Exceptions;

public sealed class MongoDbMissingConfigurationException : MagicHatException
{
    public MongoDbMissingConfigurationException() : base("MongoDb configuration is missing")
    {
    }
}
