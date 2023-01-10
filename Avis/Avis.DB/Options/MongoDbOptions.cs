using MongoDB.Driver;

namespace Avis.DB.Options;

public class MongoDbOptions
{
    public string ConnectionString { get; set; } 
    public string DatabaseName { get; set; } 
    public string UserModelDatabaseName { get; set; } 
    public Action<MongoClientSettings> MongoClientSettingsConfigurate { get; set; }
}


