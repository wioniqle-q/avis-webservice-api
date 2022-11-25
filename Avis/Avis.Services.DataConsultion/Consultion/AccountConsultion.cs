namespace Avis.Services.DataConsultion.Consultion;

public class AccountConsultion : AvisMongoDbContext
{
    public virtual async Task<OrganizationUser> OrganizationUserFindAsync(OrganizationUser organizationUser, CancellationToken cancellationToken = default)
    {
        return await GetCollection<OrganizationUser>(GetDatabaseName()).AsQueryable().OrderBy(x => x.Name).FirstOrDefaultAsync(x => x.Name == organizationUser.Name, cancellationToken);
    }

    public virtual async Task<Task<string>> OrganizationUserUpdateAsync(OrganizationUser organizationUserProperties, CancellationToken cancellationToken = default)
    {
        var result = await this.OrganizationUserFindAsync(organizationUserProperties);

        if (result is null || result.IsDeleted == true && result.IsActive == false)
        {
            return Task.FromResult("OrganizationUser not found");
        }

        var newProperty = Builders<OrganizationUser>.Update.Set(x => x.Password, organizationUserProperties.Password);
        await GetCollection<OrganizationUser>(GetDatabaseName()).UpdateOneAsync(x => x.Name == organizationUserProperties.Name, newProperty, cancellationToken: cancellationToken);

        return Task.FromResult("OrganizationUser password updated");
    }

    public virtual async Task<Task<string>> OrganizationUserCreateAsync(OrganizationUser organizationUserProperties, CancellationToken cancellationToken = default)
    {
        var result = await this.OrganizationUserFindAsync(organizationUserProperties);

        if (result is not null)
        {
            return Task.FromResult("OrganizationUser already exists");
        }

        await GetCollection<OrganizationUser>(GetDatabaseName()).InsertOneAsync(organizationUserProperties, cancellationToken: cancellationToken);

        return Task.FromResult("OrganizationUser created");
    }


    public virtual async Task<Task<string>> OrganizationUserActivateAsync(OrganizationUser organizationUserProperties, CancellationToken cancellationToken = default)
    {
        var result = await this.OrganizationUserFindAsync(organizationUserProperties);

        if (result is null)
        {
            return Task.FromResult("OrganizationUser not found");
        }

        if (result.IsActive == true)
        {
            return Task.FromResult("OrganizationUser is already active");
        }

        var newProperty = Builders<OrganizationUser>.Update.Set(x => x.IsActive, true).Set(x => x.IsDeleted, false);
        await GetCollection<OrganizationUser>(GetDatabaseName()).UpdateOneAsync(x => x.Name == result.Name, newProperty, cancellationToken: cancellationToken);

        return Task.FromResult("OrganizationUser activated");
    }

    public virtual async Task<Task<string>> OrganizationUserDeactivateAsync(OrganizationUser organizationUserProperties, CancellationToken cancellationToken = default)
    {
        var result = await this.OrganizationUserFindAsync(organizationUserProperties);


        if (result is null)
        {
            return Task.FromResult("OrganizationUser not found");
        }

        if (result.IsActive == false)
        {
            return Task.FromResult("OrganizationUser is already inactive");
        }

        var newProperty = Builders<OrganizationUser>.Update.Set(x => x.IsActive, false).Set(x => x.IsDeleted, true);
        await GetCollection<OrganizationUser>(GetDatabaseName()).UpdateOneAsync(x => x.Name == result.Name, newProperty, cancellationToken: cancellationToken);

        return Task.FromResult("OrganizationUser inactived");
    }

    protected virtual IMongoQueryable<T> GetMongoQueryable<T>(IQueryable<T> queryable)
    {
        if (queryable is IMongoQueryable<T> mongoQueryable)
        {
            return mongoQueryable;
        }

        throw new ArgumentException("The queryable is not a Mongo queryable.", nameof(queryable));
    }
}
