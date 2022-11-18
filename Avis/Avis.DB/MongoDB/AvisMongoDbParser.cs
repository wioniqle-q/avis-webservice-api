using MongoDB.Driver.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Avis.DB.MongoDB;

public class AvisMongoDbParser : IAvisMongoDbParser
{
    protected virtual IMongoQueryable<T> GetMongoQueryable<T>(IQueryable<T> queryable)
    {
        if (queryable is IMongoQueryable<T> mongoQueryable)
        {
            return mongoQueryable;
        }

        throw new ArgumentException("The queryable is not a Mongo queryable.", nameof(queryable));
    }
    
    public Task<T> FirstOrDefaultAsync<T>([NotNull] IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return GetMongoQueryable(queryable).FirstOrDefaultAsync(cancellationToken);
    }
}
