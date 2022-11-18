using System.Diagnostics.CodeAnalysis;

namespace Avis.Services.Protection.HashAlgorithmPageGroup;

public class AlgorithmPage
{
    public virtual string? Value
    {
        get => _Value;
        set => _Value = value;
    }
    private volatile string? _Value;

    public virtual string? Salt
    {
        get => _Salt;
        set => _Salt = value;
    }
    private volatile string? _Salt;

    public virtual string? Hash
    {
        get => _Hash;
        set => _Hash = value;
    }
    private volatile string? _Hash;

    public AlgorithmPage([NotNull] string value, [NotNull] string salt, [NotNull] string hash)
    {
        Value = value;
        Salt = salt;
        Hash = hash;
    }
}
