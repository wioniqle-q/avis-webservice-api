using MongoDB.Driver;

namespace Avis.DB.MongoDB;

public interface IAvisMongoDbContext
{
    IMongoCollection<T> GetCollection<T>(string collectionName);
}
