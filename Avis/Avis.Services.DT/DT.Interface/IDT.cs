namespace Avis.Services.DT.DT.Interface;

public interface IDT<T>
{
    Task<T> Encrypt(T value);
    Task<T> Decrypt(T value);
}
