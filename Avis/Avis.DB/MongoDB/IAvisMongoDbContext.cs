using MongoDB.Driver;

namespace Avis.DB.MongoDB;

public abstract class IAvisMongoDbContext
{
    public abstract IMongoCollection<T> GetCollection<T>(string collectionName);
    public abstract string GetUserModelDatabaseName();
}
