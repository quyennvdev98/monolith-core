using System.Security.Cryptography;
using System.Text;
using Monolith.Core.Domain.Enums;
using Monolith.Core.Shared.Security;


namespace Monolith.Core.Infrastructure.Security.Encryption;

public sealed class RSAEncryptor : IEncryptionStrategy
{
    public AlgorithmNameType AlgorithmType => AlgorithmNameType.RSA;

    public string Encrypt(string data, string publicKeyPem)
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(publicKeyPem.ToCharArray());

        var bytesToEncrypt = Encoding.UTF8.GetBytes(data);
        var encrypted = rsa.Encrypt(bytesToEncrypt, RSAEncryptionPadding.Pkcs1);
        return Convert.ToBase64String(encrypted);
    }

    public string Decrypt(string data, string privateKeyPem)
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(privateKeyPem.ToCharArray());

        var bytesToDecrypt = Convert.FromBase64String(data);
        var decrypted = rsa.Decrypt(bytesToDecrypt, RSAEncryptionPadding.Pkcs1);
        return Encoding.UTF8.GetString(decrypted);
    }
}