
using System.Diagnostics.CodeAnalysis;

namespace Avis.DB.MongoDB;

public interface IAvisMongoDbParser
{
    #region First/FirstOrDefault
    Task<T> FirstOrDefaultAsync<T>([NotNull] IQueryable<T> queryable, CancellationToken cancellationToken = default);
    #endregion 
}
