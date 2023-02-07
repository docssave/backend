using System.Security.Cryptography;
using System.Text;

namespace Encryption;

public sealed class Sha256Encryptor : IEncryptor
{
    public Task<string> EncryptAsync(string payload)
    {
        var bytes = Encoding.UTF8.GetBytes(payload);
        var hash = SHA256.HashData(bytes);
        var result = string.Concat(hash.Select(x => x.ToString("x2")));

        return Task.FromResult(result);
    }

    public Task<string> DecryptAsync(string payload)
    {
        throw new NotImplementedException();
    }
}