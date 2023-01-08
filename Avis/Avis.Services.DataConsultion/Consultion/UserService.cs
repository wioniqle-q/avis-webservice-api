using Avis.Services.Models;

namespace Avis.Services.DataConsultion.Consultion;

public class UserService : AvisMongoDbContext
{

    public virtual async Task<DerivedUserVirtualClass> FindUserModelAsync(DerivedUserVirtualClass userModel, CancellationToken cancellationToken = default)
    {
        var collection = GetCollection<DerivedUserVirtualClass>(GetUserModelDatabaseName());
        collection.Indexes.CreateOne(new CreateIndexModel<DerivedUserVirtualClass>(Builders<DerivedUserVirtualClass>.IndexKeys.Hashed(d => d.UserName)));

        var query = collection.AsQueryable().OrderBy(x => x.UserName).Where(x => x.UserName == userModel.UserName);
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<Task<String>> InsertUserModelAsync(DerivedUserVirtualClass userModel, CancellationToken cancellationToken = default)
    {
        var existingUser = await this.FindUserModelAsync(userModel, cancellationToken);
        if (existingUser is not null)
        {
            return Task.FromResult("User Already Exists");
        }

        var GeneratedSalt = SaltGenerator.GenerateSalt();
        var EncryptedPassword = ArgonEncryptor.ArgonHashPassword(userModel.Password, GeneratedSalt);

        var property = new DerivedUserVirtualClass()
        {
            UserName = userModel.UserName,
            Password = EncryptedPassword,
            Salt = GeneratedSalt
        };

        var session = await this.StartSessionAsync(cancellationToken);
        var result = session.WithTransactionAsync<string>(async (handle, token) =>
        {
            await GetCollection<DerivedUserVirtualClass>(GetUserModelDatabaseName()).InsertOneAsync(property, cancellationToken: cancellationToken);
            return "User Created";
        }, cancellationToken: cancellationToken);

        await session.CommitTransactionAsync(cancellationToken);

        return await Task.FromResult(result);
    }

    public virtual async Task<Task<String>> BlockUserModelAsync(DerivedUserVirtualClass userModel, CancellationToken cancellationToken = default)
    {
        var result = await this.FindUserModelAsync(userModel, cancellationToken);

        if (result == null)
        {
            return Task.FromResult("User Not Found");
        }

        if (result.IsBlocked is true && result.IsActive is false)
        {
            return Task.FromResult("User Already Blocked");
        }

        var newProperty = Builders<DerivedUserVirtualClass>.Update.Set(x => x.IsActive, false).Set(x => x.IsBlocked, true);
        await GetCollection<DerivedUserVirtualClass>(GetUserModelDatabaseName()).UpdateOneAsync(x => x.UserName == result.UserName, newProperty, cancellationToken: cancellationToken);

        return Task.FromResult("User Blocked");
    }

    public virtual async Task<Task<String>> UnblockUserModelAsync(DerivedUserVirtualClass userModel, CancellationToken cancellationToken = default)
    {
        var result = await this.FindUserModelAsync(userModel, cancellationToken);

        if (result == null)
        {
            return Task.FromResult("User Not Found");
        }

        if (result.IsBlocked is false && result.IsActive is true)
        {
            return Task.FromResult("User Already Unblocked");
        }

        var newProperty = Builders<DerivedUserVirtualClass>.Update.Set(x => x.IsActive, true).Set(x => x.IsBlocked, false);
        await GetCollection<DerivedUserVirtualClass>(GetUserModelDatabaseName()).UpdateOneAsync(x => x.UserName == result.UserName, newProperty, cancellationToken: cancellationToken);

        return Task.FromResult("User Unblocked");
    }

    public virtual async Task<Task<String>> UpdateUserModelAsync(DerivedUserVirtualClass userModel, CancellationToken cancellationToken = default)
    {
        var result = await this.FindUserModelAsync(userModel, cancellationToken);

        if (result == null)
        {
            return Task.FromResult("User Not Found");
        }

        var GeneratedSalt = SaltGenerator.GenerateSalt();
        var EncryptedPassword = ArgonEncryptor.ArgonHashPassword(userModel.Password, GeneratedSalt);

        var property = new DerivedUserVirtualClass()
        {
            Password = EncryptedPassword,
            Salt = GeneratedSalt
        };

        var newProperty = Builders<DerivedUserVirtualClass>.Update.Set(x => x.Password, property.Password).Set(x => x.Salt, property.Salt);
        await GetCollection<DerivedUserVirtualClass>(GetUserModelDatabaseName()).UpdateOneAsync(x => x.UserName == result.UserName, newProperty, cancellationToken: cancellationToken);
    
        return Task.FromResult("User Updated");
    }
    
    public virtual async Task<Task<String>> DeleteUserModelAsync(DerivedUserVirtualClass userModel, CancellationToken cancellationToken = default)
    {
        var result = await this.FindUserModelAsync(userModel, cancellationToken);

        if (result == null)
        {
            return Task.FromResult("User Not Found");
        }

        await GetCollection<DerivedUserVirtualClass>(GetUserModelDatabaseName()).DeleteOneAsync(x => x.UserName == result.UserName, cancellationToken);

        return Task.FromResult("User Deleted");
    }
}

