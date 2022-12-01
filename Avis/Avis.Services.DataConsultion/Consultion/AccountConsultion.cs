﻿namespace Avis.Services.DataConsultion.Consultion;

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
        
        var generateSalt = HashLoader.HashCreate();
        var HashedPassword = HashLoader.HashCreate(organizationUserProperties.Password, generateSalt);
        var organizationUser = new OrganizationUser(String.Empty, HashedPassword);

        var newProperty = Builders<OrganizationUser>.Update.Set(x => x.Password, organizationUser.Password);
        await GetCollection<OrganizationUser>(GetDatabaseName()).UpdateOneAsync(x => x.Name == organizationUserProperties.Name, newProperty, cancellationToken: cancellationToken);

        return Task.FromResult("OrganizationUser password updated");
    }

    public virtual async Task<Task<string>> OrganizationUserCreateAsync(OrganizationUser organizationUserProperties, CancellationToken cancellationToken = default)
    {
        var result = await this.OrganizationUserFindAsync(organizationUserProperties, cancellationToken);

        if (result is not null)
        {
            return Task.FromResult("OrganizationUser already exists");
        }

        var generateSalt = HashLoader.HashCreate();
        var HashedPassword = HashLoader.HashCreate(organizationUserProperties.Password, generateSalt);
        var organizationUser = new OrganizationUser(organizationUserProperties.Name, HashedPassword);

        await GetCollection<OrganizationUser>(GetDatabaseName()).InsertOneAsync(organizationUser, cancellationToken: cancellationToken);

        return Task.FromResult("OrganizationUser created");
    }

    public virtual async Task<Task<string>> OrganizationUserActivateAsync(OrganizationUser organizationUserProperties, CancellationToken cancellationToken = default)
    {
        var result = await this.OrganizationUserFindAsync(organizationUserProperties, cancellationToken);

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
        var result = await this.OrganizationUserFindAsync(organizationUserProperties, cancellationToken);

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

    public virtual async Task<List<OrganizationUser>> OrganizationUserMapAsync(CancellationToken cancellationToken = default)
    {
        return await GetCollection<OrganizationUser>(GetDatabaseName()).AsQueryable().OrderBy(x => x.Name).Take(100).ToListAsync(cancellationToken);
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
