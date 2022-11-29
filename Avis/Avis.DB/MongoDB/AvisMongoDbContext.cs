
using MongoDB.Driver;

namespace Avis.DB.MongoDB;

public class AvisMongoDbContext : IAvisMongoDbContext
{
    protected IMongoDatabase MongoDatabase { get; private set; }

    public AvisMongoDbContext()
    {
        var clientSettings = MongoClientSettings.FromConnectionString("");
        clientSettings.ServerApi = new ServerApi(ServerApiVersion.V1);
        clientSettings.MaxConnectionIdleTime = new TimeSpan(0, 5, 0);

        var client = new MongoClient(clientSettings);
        MongoDatabase = client.GetDatabase("");
    }

    public virtual IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return MongoDatabase.GetCollection<T>(collectionName);
    }

    public virtual string GetDatabaseName()
    {
        return MongoDatabase.DatabaseNamespace.DatabaseName;
    }
}