namespace Idn.Domain.V1.Services;

internal sealed class DummyEncryptor : IEncryptor
{
    public Task<string> EncryptAsync(string value)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(value);

        return Task.FromResult(Convert.ToBase64String(plainTextBytes));
    }

    public Task<string> DecryptAsync(string value)
    {
        var base64EncodedBytes = Convert.FromBase64String(value);

        return Task.FromResult(System.Text.Encoding.UTF8.GetString(base64EncodedBytes));
    }
}