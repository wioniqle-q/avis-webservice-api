using Avis.Services.DT.DT.Interface;
using System.Security.Cryptography;
using System.Text;

namespace Avis.Services.DT.DT.Algorithm;

public class ProtectDT : IDT<string>
{
    private protected static volatile string encryptionKey = "";

    public virtual async Task<string> Encrypt(string value)
    {
        byte[] GetBytes = Encoding.Unicode.GetBytes(value);
        using Aes aes = Aes.Create();

        Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(encryptionKey, new byte[]
        {
          0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        })
        {
            IterationCount = 150000
        };
        
        aes.Key = rfc2898DeriveBytes.GetBytes(32);
        aes.IV = rfc2898DeriveBytes.GetBytes(16);

        using MemoryStream memoryStream = new MemoryStream();
        using CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(GetBytes, 0, GetBytes.Length);
        cryptoStream.FlushFinalBlock();
        cryptoStream.Close();

        byte[] array = memoryStream.ToArray();
        return await Task.FromResult(Convert.ToBase64String(array));
    }

    public virtual async Task<string> Decrypt(string value)
    {
        byte[] array = Convert.FromBase64String(value.Replace(" ", "+"));
        using Aes aes = Aes.Create();

        Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(encryptionKey, new byte[]
        {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        })
        {
            IterationCount = 150000
        };
        
        aes.Key = rfc2898DeriveBytes.GetBytes(32);
        aes.IV = rfc2898DeriveBytes.GetBytes(16);
 
        using MemoryStream memoryStream = new MemoryStream();
        using CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(array, 0, array.Length);
        cryptoStream.FlushFinalBlock();
        cryptoStream.Close();

        byte[] bytes = memoryStream.ToArray();
        return await Task.FromResult(Encoding.Unicode.GetString(bytes));
    }
}