namespace Idn.Domain.V1.Services;

internal interface IEncryptor
{
    Task<string> EncryptAsync(string value);

    Task<string> DecryptAsync(string value);
}