namespace MagicHat.MongoDB.Configuration;

public sealed class MongoConfiguration
{
    public MongoConfiguration()
    {
    }
    
    public MongoConfiguration(string url, string database)
    {
        Url = url;
        Database = database;
    }

    public string Url { get; set; }
    public string Database { get; set; }
    public uint ConnectTimeout { get; set; } = 4000;
    public uint SocketTimeout { get; set; } = 4000;
}
