namespace Idn.Domain.Services;

internal interface IEncryptor
{
    Task<string> EncryptAsync(string value);

    Task<string> DecryptAsync(string value);
}