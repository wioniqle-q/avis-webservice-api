namespace Avis.Services.DataConsultion.Consultion;

public class AuthenticateConsultion : AvisMongoDbContext
{
    protected private AccountConsultion AccountConsultion { get; }
    protected private ProtectDT ProtectDT { get; }

    public AuthenticateConsultion(AccountConsultion accountConsultion, ProtectDT protectDT)
    {
        this.AccountConsultion = accountConsultion;
        this.ProtectDT = protectDT;
    }

    public virtual async Task<string> AuthenticateAsync(OrganizationUser organizationUser, CancellationToken cancellationToken = default)
    {
        ValidateAuthentication(organizationUser);

        if (await CheckStringIsNotBase64Encoded(organizationUser.Name) is not true || await CheckStringIsNotBase64Encoded(organizationUser.Password) is not true)
        {
            return Task.FromResult("Text validation is invalid. please contact the producer.").Result;
        }

        return await ValidateHashedPassword(organizationUser, cancellationToken);
    }

    protected virtual async Task<string> ValidateHashedPassword(OrganizationUser organizationUser, CancellationToken cancellationToken)
    {
        var userOrganization = await ValidateDTLayer(organizationUser, cancellationToken);
        var result = await AccountConsultion.OrganizationUserFindAsync(userOrganization, cancellationToken);

        if (result is null || result.IsDeleted == true && result.IsActive == false)
        {
            return await Task.FromResult("User not found");
        }

        var validate = await ValidateDBAuthentication(result.Password, userOrganization);
        return validate;
    }

    protected async virtual Task<OrganizationUser> ValidateDTLayer(OrganizationUser organizationUser, CancellationToken cancellationToken)
    {
        DTPage dTPage = new(await ProtectDT.Decrypt(organizationUser.Name), await ProtectDT.Decrypt(organizationUser.Password));

        return await Task.FromResult(new OrganizationUser(dTPage.Name, dTPage.Password));
    }

    protected virtual Task<string> ValidateDBAuthentication(string dbPassword, OrganizationUser organizationUser)
    {
        var page = new AlgorithmPage(dbPassword);

        if (HashLoader.ValidateHash(organizationUser.Password, page.Value.Split('æ')[1], page.Value.Split('æ')[0]) is false)
        {
            return Task.FromResult("Password is incorrect");
        }

        return Task.FromResult("Authenticated");
    }

    protected virtual void ValidateAuthentication(OrganizationUser organizationUser)
    {
        if (organizationUser is null)
        {
            throw new ArgumentNullException(nameof(organizationUser));
        }

        if (string.IsNullOrWhiteSpace(organizationUser.Name))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(organizationUser.Name));
        }

        if (string.IsNullOrWhiteSpace(organizationUser.Password))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(organizationUser.Password));
        }
    }

    protected virtual async Task<bool> CheckStringIsNotBase64Encoded(string input)
    {
        if (input.Length % 4 != 0 || input.Contains(" ") || input.Contains("\t") || input.Contains("\r") || input.Contains("\n") || input.Contains("'") || input.Contains("\"") || input.Contains(";") || input.Contains("--") || input.Contains("/*") || input.Contains("*/"))
        {
            return await Task.FromResult(false);
        }

        try
        {
            await ProtectDT.Decrypt(input);
        }
        catch (Exception)
        {
            return await Task.FromResult(false);
        }

        return await Task.FromResult(true);
    }
}
