using Microsoft.Extensions.DependencyInjection;

namespace Avis.DB.Interfaces;
public interface IMongoDbModule 
{
    void ConfigureServices(IServiceCollection services);
}
