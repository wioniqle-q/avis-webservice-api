using Avis.Services.DataConsultion.Consultion.Response;
using Avis.Services.DT.TransferObjects;
using Avis.Services.Models;

namespace Avis.Services.DataConsultion.Consultion;

public class AuthService : AvisMongoDbContext
{
    private UserService @userService { get; }

    public AuthService(UserService userService)
    {
        this.userService = userService;
    }

    public virtual async Task<AuthServiceResponse> AuthenticateUserAsync(DerivedUserVirtualClass userModel, CancellationToken cancellationToken = default)
    {
        var result = await ValidateUserPassword(userModel, cancellationToken);
        return await Task.FromResult(new AuthServiceResponse() { Response = string.Concat(result.Response) });
    }

    protected virtual async Task<AuthServiceResponse> ValidateUserPassword(DerivedUserVirtualClass userModel, CancellationToken cancellationToken = default)
    {
        var userProperty = await DecryptUserProperty(userModel, cancellationToken);

        var queryUser = await this.userService.FindUserModelAsync(userProperty, cancellationToken);
        if (queryUser is null || queryUser.IsBlocked || !queryUser.IsActive)
        {
            return await Task.FromResult(new AuthServiceResponse() { Response = "User is not found" });
        }

        var validation = await this.ValidateDBPassword(userProperty.Password, queryUser);
        return validation;
    }
    
    protected virtual async Task<AuthServiceResponse> ValidateDBPassword(string password, DerivedUserVirtualClass userModel)
    {
        if (PasswordValidator.ArgonVerifyPassword(password, userModel.Salt, userModel.Password) is false)
        {
            return new AuthServiceResponse() { Response = "Password is incorrect" };
        }

        return await Task.FromResult(new AuthServiceResponse() { Response = "Authenticated" });
    }

    protected async Task<DerivedUserVirtualClass> DecryptUserProperty(DerivedUserVirtualClass userModel, CancellationToken cancellationToken = default)
    {
        return new DerivedUserVirtualClass() { UserName = await AesEncryption.Decrypt(userModel.UserName, cancellationToken), Password = await AesEncryption.Decrypt(userModel.Password, cancellationToken) };
    }
}
