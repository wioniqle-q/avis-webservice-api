using System.Security.Cryptography;
using System.Text;

namespace Avis.Services.DT.TransferObjects;

public static class AesEncryption
{
    private static readonly string encryptionKey = "";
    private static readonly byte[] salt = new byte[]
    {
        0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
    };

    public static async Task<string> Encrypt(string value, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        byte[] plaintext = Encoding.Unicode.GetBytes(value);
        using Aes aes = Aes.Create();
        aes.KeySize = 256;

        using Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(encryptionKey, salt, 1000 * 150);
        aes.Key = rfc2898.GetBytes(32);
        aes.IV = rfc2898.GetBytes(16);

        using MemoryStream memoryStream = new MemoryStream();
        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
        {
            cryptoStream.Write(plaintext, 0, plaintext.Length);
            cryptoStream.FlushFinalBlock();
        }

        return await Task.FromResult(Convert.ToBase64String(memoryStream.ToArray()));
    }

    public static async Task<string> Decrypt(string value, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        byte[] ciphertext = Convert.FromBase64String(value.Replace(" ", "+"));
        using Aes aes = Aes.Create();
        aes.KeySize = 256;

        using Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(encryptionKey, salt, 1000 * 150);
        aes.Key = rfc2898.GetBytes(32);
        aes.IV = rfc2898.GetBytes(16);

        using MemoryStream memoryStream = new MemoryStream();
        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
        {
            cryptoStream.Write(ciphertext, 0, ciphertext.Length);
            cryptoStream.FlushFinalBlock();
        }

        return await Task.FromResult(Encoding.Unicode.GetString(memoryStream.ToArray()));
    }
}