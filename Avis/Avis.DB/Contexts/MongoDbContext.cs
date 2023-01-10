using Avis.DB.Configurations;
using Avis.DB.Interfaces;
using MongoDB.Driver;

namespace Avis.DB.Contexts;

public class MongoDbContext : IMongoDbContext
{
    private IMongoClient _client;
    public IMongoDatabase Database { get; }
    
    public MongoDbContext(MongoDbConfigurations configurations)
    {
        _client = configurations.MongoClient;
        Database = configurations.Database;
    }

    public virtual IMongoCollection<T> GetCollection<T>(string name)
    {
        return Database.GetCollection<T>(name);
    }

    public virtual string GetUserModelDatabaseName()
    {
        return Database.DatabaseNamespace.DatabaseName;
    }

    public async Task<IClientSessionHandle> StartSessionAsync(CancellationToken cancellationToken = default)
    {
        var options = new ClientSessionOptions
        {
            CausalConsistency = true,
            DefaultTransactionOptions = new TransactionOptions(
                readConcern: ReadConcern.Majority,
                writeConcern: WriteConcern.WMajority)
        };

        return await _client.StartSessionAsync(options, cancellationToken);
    }
}
