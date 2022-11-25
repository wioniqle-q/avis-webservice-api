using System.Diagnostics.CodeAnalysis;

namespace Avis.Services.Protection.HashAlgorithmPageGroup;

public class AlgorithmPage
{
    public virtual string Value
    {
        get => _Value;
        set => _Value = value;
    }
    private volatile string _Value;

    public AlgorithmPage([NotNull] string value)
    {
        Value = value;
    }
}
