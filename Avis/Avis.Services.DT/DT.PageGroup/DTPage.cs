using System.Diagnostics.CodeAnalysis;

namespace Avis.Services.DT.DT.PageGroup;

public class DTPage
{
    public virtual string Name
    {
        get => _Name;
        set => _Name = value;
    }
    private volatile string _Name;

    public virtual string Password
    {
        get => _Password;
        set => _Password = value;
    }
    private volatile string _Password;

    public DTPage([NotNull] string name, [NotNull] string password)
    {
        Name = name;
        Password = password;
    }
}
