using System.Security.Cryptography;

namespace Avis.Services.Protection.EncryptionAlgorithm.Encryption;

public static class SaltGenerator
{
    private const int SaltLength = 2048 / 8; // 256 bits
    
    public static string GenerateSalt()
    {
        var saltBytes = new byte[SaltLength];
        
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }

        return Convert.ToBase64String(saltBytes);
    }
}

