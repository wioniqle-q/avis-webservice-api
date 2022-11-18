using Avis.DB.MongoDB;
using Avis.Services.OrganizationModel;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Avis.Services.DataConsultion.Consultion;

public class AccountConsultion : AvisMongoDbContext
{
    public virtual async Task<OrganizationUser> OrganizationUserFindAsync(OrganizationUser organizationUser)
    {
        var collection = GetCollection<OrganizationUser>(GetDatabaseName());
        var result = await collection.AsQueryable().Where(x => x.Name == organizationUser.Name).FirstOrDefaultAsync();
        return result;
    }

    public virtual async Task<Task<string>> OrganizationUserUpdateAsync(OrganizationUser organizationUserProperties)
    {
        var collection = GetCollection<OrganizationUser>(GetDatabaseName());
        var result = await this.OrganizationUserFindAsync(organizationUserProperties);

        if (result is null)
        {
            return Task.FromResult("OrganizationUser not found");
        }

        if (result.IsDeleted == true && result.IsActive == false)
        {
            return Task.FromResult("You cannot update a deleted OrganizationUser");
        }

        var newProperty = Builders<OrganizationUser>.Update.Set(x => x.Password, organizationUserProperties.Password);
        await collection.UpdateOneAsync(x => x.Name == result.Name, newProperty);

        return Task.FromResult("OrganizationUser password updated");
    }

    public virtual async Task<Task<string>> OrganizationUserCreateAsync(OrganizationUser organizationUserProperties)
    {
        var collection = GetCollection<OrganizationUser>(GetDatabaseName());
        var result = await this.OrganizationUserFindAsync(organizationUserProperties);

        if (result is not null)
        {
            return Task.FromResult("OrganizationUser already exists");
        }

        await collection.InsertOneAsync(organizationUserProperties);

        return Task.FromResult("OrganizationUser created");
    }

    public virtual async Task<Task<string>> OrganizationUserActivateAsync(OrganizationUser organizationUserProperties)
    {
        var collection = GetCollection<OrganizationUser>(GetDatabaseName());
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
        await collection.UpdateOneAsync(x => x.Name == result.Name, newProperty);

        return Task.FromResult("OrganizationUser activated");
    }

    public virtual async Task<Task<string>> OrganizationUserDeactivateAsync(OrganizationUser organizationUserProperties)
    {
        var collection = GetCollection<OrganizationUser>(GetDatabaseName());
        var result = await this.OrganizationUserFindAsync(organizationUserProperties);

        if (result is null)
        {
            return Task.FromResult("OrganizationUser not found");
        }

        if (result.IsActive == false)
        {
            return Task.FromResult("OrganizationUser is already deactivated");
        }

        var newProperty = Builders<OrganizationUser>.Update.Set(x => x.IsActive, false).Set(x => x.IsDeleted, true);
        await collection.UpdateOneAsync(x => x.Name == result.Name, newProperty);

        return Task.FromResult("OrganizationUser deactivated");
    }

    public virtual async Task<List<OrganizationUser>> OrganizationUserGetAllAsync()
    {
        var collection = GetCollection<OrganizationUser>(GetDatabaseName());
        var result = await collection.AsQueryable().ToListAsync();
        return result;
    }

    public virtual async Task<List<OrganizationUser>> OrganizationUserGetAllByNameAsync(OrganizationUser organization)
    {
        var collection = GetCollection<OrganizationUser>(GetDatabaseName());
        var result = await collection.AsQueryable().Where(x => x.Name == organization.Name).ToListAsync();
        return result;
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
