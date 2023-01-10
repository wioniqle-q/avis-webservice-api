using System.Text;

using Konscious.Security.Cryptography;

namespace Avis.Services.Protection.EncryptionAlgorithm.Encryption;

/// <summary>
/// Most used Argon2id encryption settings.
/// DegreeOfParallelism = 8
/// MemorySize = 524288 // 512 MB
/// Iterations = 8
/// </summary>
enum ArgonEncryptorEnums
{
    DegreeOfParallelism = 4,
    MemorySize = 65536, // 64 MB
    Iterations = 4
}

public sealed class ArgonEncryptor
{
    public static string ArgonHashPassword(string password, string salt)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            DegreeOfParallelism = (int)ArgonEncryptorEnums.DegreeOfParallelism,
            MemorySize = (int)ArgonEncryptorEnums.MemorySize,
            Iterations = (int)ArgonEncryptorEnums.Iterations,
            Salt = Encoding.UTF8.GetBytes(salt)
        };

        return Convert.ToBase64String(argon2.GetBytes(32));
    }
}
