using MongoDB.Driver;

namespace Avis.DB.Interfaces;
public interface IMongoDbContext
{
    IMongoDatabase Database { get; }
    IMongoCollection<T> GetCollection<T>(string name);
}
