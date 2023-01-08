using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Avis.DB.MongoDB;

public class AvisMongoDbContext : IAvisMongoDbContext
{
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IMongoClient _client;
    
    private static readonly string ConnectionString = "";

    public AvisMongoDbContext()
    {
        var clientSettings = MongoClientSettings.FromConnectionString(ConnectionString);
        clientSettings.MaxConnectionPoolSize = 100;
        clientSettings.MinConnectionPoolSize = 10;

        clientSettings.ServerApi = new ServerApi(ServerApiVersion.V1);
        clientSettings.MaxConnectionIdleTime = TimeSpan.FromMinutes(5);

        clientSettings.ReadConcern = ReadConcern.Majority;
        clientSettings.WriteConcern = WriteConcern.WMajority;
        clientSettings.LinqProvider = LinqProvider.V3;

        var client = new MongoClient(clientSettings);
        _mongoDatabase = client.GetDatabase("");
        _client = client;
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

    public override IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _mongoDatabase.GetCollection<T>(collectionName);
    }

    public override string GetUserModelDatabaseName()
    {
        return _mongoDatabase.DatabaseNamespace.DatabaseName;
    }
}
