using System.Diagnostics.CodeAnalysis;

namespace Avis.Services.OrganizationModel;

public class OrganizationUser : OrganizationUserProperties
{
    public virtual string? Name
    {
        get => name;
        set => name = value;
    }
    private volatile string? name;

    public virtual string? Password
    {
        get => password;
        set => password = value;
    }
    private volatile string? password;

    public OrganizationUser([NotNull] string name, [NotNull] string password)
    {
        Name = name;
        Password = password;
    }
}