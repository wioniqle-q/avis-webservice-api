using Microsoft.Extensions.Configuration;
using Avis.DB.Configurations;
using Avis.DB.Contexts;
using Avis.DB.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Avis.DB.Modules;

public class MongoDbModule : IMongoDbModule
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IMongoDbContext, MongoDbContext>();
        services.AddTransient<MongoDbConfigurations>();
    }
}
