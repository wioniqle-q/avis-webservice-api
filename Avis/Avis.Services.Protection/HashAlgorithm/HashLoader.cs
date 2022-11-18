namespace Avis.Services.Protection.HashAlgorithm;

public static class HashLoader
{
    public static string HashCreate(string value, string salt)
    {
        var Bytes = Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivation.Pbkdf2(
                                 password: value,
                                 salt: System.Text.Encoding.UTF8.GetBytes(salt),
                                 prf: Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivationPrf.HMACSHA512,
                                 iterationCount: 150000,
                                 numBytesRequested: 256 / 8);

        return Convert.ToBase64String(Bytes) + "æ" + salt;
    }

    public static bool ValidateHash(string value, string salt, string hash)
           => HashCreate(value, salt).Split('æ')[0] == hash;

    public static string HashCreate()
    {
        byte[] Bytes = new byte[128 / 8];
        using var numberGenerator = System.Security.Cryptography.RandomNumberGenerator.Create();
        numberGenerator.GetBytes(Bytes);
        return Convert.ToBase64String(Bytes);
    }
}
