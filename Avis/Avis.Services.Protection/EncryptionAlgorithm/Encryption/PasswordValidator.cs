namespace Avis.Services.Protection.EncryptionAlgorithm.Encryption;

public static class PasswordValidator
{
    public static bool ArgonVerifyPassword(string password, string storedSalt, string storedHash)
    {
        return ArgonEncryptor.ArgonHashPassword(password, storedSalt) == storedHash;
    }
}

