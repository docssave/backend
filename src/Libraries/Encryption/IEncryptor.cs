namespace Encryption;

public interface IEncryptor
{
    Task<string> EncryptAsync(string payload);

    Task<string> DecryptAsync(string payload);
}