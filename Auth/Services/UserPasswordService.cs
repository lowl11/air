using System.Security.Cryptography;
using System.Text;
using Auth.Data.Interfaces.Services;

namespace Auth.Services;

public class UserPasswordService : IUserPasswordService
{

    private readonly string _key;
    private string? _encryptedPassword;
    private string? _decryptedPassword;
    
    public UserPasswordService(string key)
    {
        if (key == string.Empty) throw new ArgumentNullException(nameof(key));
        _key = key;
    }

    public string Encrypt(string password)
    {
        if (_encryptedPassword is not null) return _encryptedPassword;
        _encryptedPassword = EncryptString(_key, password);
        return _encryptedPassword;
    }

    public string Decrypt(string encryptedPassword)
    {
        if (_decryptedPassword is not null) return _decryptedPassword;
        _decryptedPassword = DecryptString(_key, encryptedPassword);
        return _decryptedPassword;
    }
    
    private static string EncryptString(string key, string plainText)
    {
        var iv = new byte[16];
        byte[] array;

        using (var aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(array);
    }
    
    private static string DecryptString(string key, string cipherText)
    {
        var iv = new byte[16];
        var buffer = Convert.FromBase64String(cipherText);

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = iv;
        var decryptTransformer = aes.CreateDecryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream(buffer);
        using var cryptoStream = new CryptoStream(memoryStream, decryptTransformer, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);
        return streamReader.ReadToEnd();
    }
    
}