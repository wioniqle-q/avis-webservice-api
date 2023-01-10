using Avis.DB.Interfaces;
using Avis.DB.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Avis.DB.Configurations;

public class MongoDbConfigurations : IMongoDbContext
{
    private readonly MongoDbOptions _options;
    private readonly MongoClient _mongoClient;

    public MongoDbConfigurations(IOptions<MongoDbOptions> options)
    {
        _options = options.Value;
        _mongoClient = CreateMongoClient(new MongoUrl(_options.ConnectionString));
    }

    public IMongoClient MongoClient => _mongoClient;
    public IMongoDatabase Database => _mongoClient.GetDatabase(_options.DatabaseName);

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return Database.GetCollection<T>(name);
    }

    public string GetUserModelDatabaseName() => _options.UserModelDatabaseName;
    private MongoClient CreateMongoClient(MongoUrl mongoUrl)
    {
        var mongoClientSettings = MongoClientSettings.FromUrl(mongoUrl);

        mongoClientSettings.MaxConnectionPoolSize = 100;
        mongoClientSettings.MinConnectionPoolSize = 10;

        mongoClientSettings.ServerApi = new ServerApi(ServerApiVersion.V1);
        mongoClientSettings.MaxConnectionIdleTime = TimeSpan.FromMinutes(5);

        mongoClientSettings.ReadConcern = ReadConcern.Majority;
        mongoClientSettings.WriteConcern = WriteConcern.WMajority;
        mongoClientSettings.LinqProvider = LinqProvider.V3;

        return new MongoClient(mongoClientSettings);
    }
}
